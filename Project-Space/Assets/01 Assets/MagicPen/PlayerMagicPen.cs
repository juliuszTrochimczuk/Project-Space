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
        [SerializeField] private LayerMask mask;

        [SerializeField] private int poolIndex;

        private void Start()
        {
            inputController.inputActions.ActionMap.BasicAction.performed += _ => DrawObject();
        }

        private void Update()
        {
            poolIndex += Mathf.RoundToInt(inputController.inputActions.ActionMap.ControllingAction.ReadValue<float>() / 120);
            if (poolIndex < 0) poolIndex = objectsPool.pools.Count - 1;
            else if (poolIndex >= objectsPool.pools.Count) poolIndex = 0;
            ui.ActualizeBackground(poolIndex);
        }

        private void DrawObject()
        {
            if (Physics.Raycast(mainCam.position, mainCam.forward, out RaycastHit hit, Mathf.Infinity, mask))
            {
                if (objectsPool.GetObjectFromPool(poolIndex, hit.point))
                    ui.ControllCounter(poolIndex);
            }
        }
    }
}