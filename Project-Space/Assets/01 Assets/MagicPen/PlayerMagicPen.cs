using UnityEngine;
using Zenject;

namespace MagicPen
{
    public class PlayerMagicPen : MonoBehaviour
    {
        [Inject] private InputController inputController;
        [Inject] private MagicObjectsPool objectsPool;
        [Inject] private MagicPenUI ui;

        [SerializeField] private Transform mainCam;
        [SerializeField] private LayerMask drawMask;
        [SerializeField] private LayerMask wipeOffMask;

        private int poolIndex;

        private void Start()
        {
            inputController.inputActions.ActionMap.BasicAction.performed += _ => DrawObject();
            inputController.inputActions.ActionMap.SpecialAction.performed += _ => WipeOffObject();
        }

        private void Update()
        {
            poolIndex += Mathf.RoundToInt(inputController.inputActions.ActionMap.ControllingAction.ReadValue<float>() / 120);
            if (poolIndex < 0) poolIndex = objectsPool.GetNumberOfPools() - 1;
            else if (poolIndex >= objectsPool.GetNumberOfPools()) poolIndex = 0;
            ui.ActualizeBackground(poolIndex);
        }

        private void DrawObject()
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, drawMask))
            {
                GameObject objectFromPool = objectsPool.GetObjectFromPool(poolIndex);
                if (objectFromPool == null) return;
                if (!objectFromPool.TryGetComponent(out BoxCollider collider)) return;
                if (hit.normal == Vector3.up) objectFromPool.transform.position = hit.point + (Vector3.up / 2 * collider.size.y);
                else if (hit.normal == -Vector3.up) objectFromPool.transform.position = hit.point + (-Vector3.up / 2 * collider.size.y);
                else if (hit.normal == Vector3.forward) objectFromPool.transform.position = hit.point + (Vector3.forward / 2 * collider.size.z);
                else if (hit.normal == -Vector3.forward) objectFromPool.transform.position = hit.point + (-Vector3.forward / 2 * collider.size.z);
                else if (hit.normal == Vector3.right) objectFromPool.transform.position = hit.point + (Vector3.right / 2 * collider.size.x);
                else if (hit.normal == -Vector3.right) objectFromPool.transform.position = hit.point + (-Vector3.right / 2 * collider.size.x);
                ui.ControllCounter(poolIndex, -1);
            }
        }

        private void WipeOffObject()
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, wipeOffMask))
            {
                objectsPool.ReturnObjectToPool(out int usedPoolIndex, hit.collider.gameObject);
                ui.ControllCounter(usedPoolIndex, 1);
            }
        }
    }
}