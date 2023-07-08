using System;
using UnityEngine;

namespace ThemeMechanics.MagicPen
{
    public class Drawing : PenInteractable
    {
        public override bool ObjectExists { get => throw new NotImplementedException(); }
        public override bool CanBeDestroyed { get => true; }
        public override bool CanBeInstantiated { get => true; }

        private bool isDrawing = false;
        [SerializeField] private LineRenderer lineRenderer;

        public override void Destroy() => Destroy(gameObject);

        public override void Instantiate(Vector3 position, Quaternion rotation, Transform parent) => Instantiate(position, rotation);

        public override void Instantiate(Vector3 position, Quaternion rotation)
        {
            Instantiate<Drawing>(this).lineRenderer.SetPosition(0, position);
        }

        private void StopDrawing() //TODO: has to be invoked when interaction button is released
        {
            if (isDrawing)
            {
                isDrawing = false;
            }
        }

        private void FixedUpdate()
        {
            if (isDrawing)
            {
                if(Physics.Raycast(player, dir, out RaycastHit hit))
                {
                    lineRenderer.SetPosition(lineRenderer.positionCount, hit.point);
                }
            }
        }
    }
}
