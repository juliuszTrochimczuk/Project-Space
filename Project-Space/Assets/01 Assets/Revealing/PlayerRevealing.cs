using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerRevealing : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float angle;
        [SerializeField] private Transform mainCam;
        Reveal.RevealObject revealObject = null;

        private void Update()
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, range, mask))
            {
                if (Vector3.Angle(mainCam.forward, hit.point) <= angle)
                {
                    Debug.Log(hit.collider.name);
                    revealObject = hit.collider.GetComponent<Reveal.RevealObject>();
                    revealObject.isSeeing = true;
                    revealObject.StartRevealing();
                }
            }
            else
            {
                if (revealObject != null) revealObject.isSeeing = false;
            }
        }
    }
}