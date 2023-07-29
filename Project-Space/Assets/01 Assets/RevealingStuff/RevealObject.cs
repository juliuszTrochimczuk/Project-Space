using UnityEngine;

namespace Reveal
{
    public class RevealObject : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        private Material revelMaterial;

        private void Awake() => revelMaterial = GetComponent<MeshRenderer>().material;

        private void Update() => revelMaterial.SetVector("_PlayerPosition", playerTransform.position);
    }
}