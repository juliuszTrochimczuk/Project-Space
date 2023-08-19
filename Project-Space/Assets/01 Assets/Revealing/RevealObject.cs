using System.Collections;
using UnityEngine;

namespace Reveal
{
    public class RevealObject : MonoBehaviour
    {
        private Material revelMaterial;
        private Coroutine revealCoroutine;
        private Coroutine disapearCoroutine;
        [SerializeField] private float requiredTimeToAppear;
        [SerializeField] private float requiredTimeToDisapear;
        private float timeToDisappear;
        public bool isSeeing;

        private void Awake()
        {
            revelMaterial = GetComponent<MeshRenderer>().material;
            revelMaterial.SetFloat("_Alpha", 1);
        }

        private void Update()
        {
            if (revelMaterial.GetFloat("_Alpha") == 1 || revelMaterial.GetFloat("_Alpha") == 0) return;
            if (revealCoroutine != null) return;
            if (disapearCoroutine != null) return;
            if (!isSeeing) disapearCoroutine = StartCoroutine(Disapearing());
        }

        public void StartRevealing()
        {
            if (revealCoroutine == null) revealCoroutine = StartCoroutine(Revealing());
        }

        private IEnumerator Revealing()
        {
            float timeToAppear = 0.0f;
            float oldAlpha = revelMaterial.GetFloat("_Alpha");
            while (timeToAppear <= requiredTimeToAppear)
            {
                timeToAppear += Time.deltaTime;
                revelMaterial.SetFloat("_Alpha", Mathf.Lerp(oldAlpha, 0, timeToAppear / requiredTimeToAppear));
                yield return new WaitForEndOfFrame();
                if (!isSeeing) break;
            }
            if (isSeeing) revelMaterial.SetFloat("_Alpha", 0);
            revealCoroutine = null;
        }

        private IEnumerator Disapearing()
        {
            float timeToDisappear = 0.0f;
            float oldAlpha = revelMaterial.GetFloat("_Alpha");
            while (timeToDisappear <= requiredTimeToDisapear)
            {
                timeToDisappear += Time.deltaTime;
                revelMaterial.SetFloat("_Alpha", Mathf.Lerp(oldAlpha, 1, timeToDisappear / requiredTimeToDisapear));
                yield return new WaitForEndOfFrame();
                if (isSeeing) break;
            }
            if (!isSeeing) revelMaterial.SetFloat("_Alpha", 1);
            disapearCoroutine = null;
        }
    }
}