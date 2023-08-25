using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerRevealing : MonoBehaviour
    {
        [Inject] private Reveal.RevealObjectPool objectsPool;

        [SerializeField] private float range;
        [SerializeField] private float angle;
        [SerializeField] private LayerMask mask;
        [SerializeField] private Transform mainCam;

        private Reveal.RevealObject revealObject = null;

        private void Update()
        {
            foreach (Reveal.RevealObject @object in objectsPool.pool)
            {
                if (RevealConditions(@object.transform))
                {
                    revealObject = @object;
                    revealObject.isSeeing = true;
                    revealObject.StartRevealing();
                }
                else if (revealObject != null) revealObject.isSeeing = false;
            }
        }

        private bool RevealConditions(Transform objectToCheck)
        {
            float angleBetween = Vector3.Angle(mainCam.forward, objectToCheck.position);
            float distanceBetween = Vector3.Distance(transform.position, objectToCheck.position);

            if (angleBetween > angle) return false;
            if (distanceBetween > range) return false;

            Vector3 direction = objectToCheck.position - transform.position;
            return !Physics.Raycast(mainCam.transform.position, direction.normalized, range, mask);
        }
    }
}