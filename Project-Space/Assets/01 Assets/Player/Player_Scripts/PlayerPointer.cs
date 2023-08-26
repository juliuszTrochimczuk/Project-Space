using UnityEngine;

namespace Player
{
    public class PlayerPointer : MonoBehaviour
    {
        [SerializeField] private Transform mainCam;
        [SerializeField] private GameObject objectPointer;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float range;

        void Update()
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, range, mask)) objectPointer.transform.position = hit.point;
            else objectPointer.transform.position = mainCam.position + (mainCam.forward * range);
            objectPointer.transform.LookAt(mainCam);
        }
    }
}