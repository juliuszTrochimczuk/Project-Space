using UnityEngine;

#nullable enable
namespace ThemeMechanics.MagicPen
{
    public enum OperationType { None, Draw, Destroy }
    public class MagicPen : MonoBehaviour
    {
        public PenInteractable? selected;
        public OperationType selectedOperation;
        private RaycastHit hit;

        void Update()
        {
            if (selectedOperation != OperationType.None)
            {
                if (Physics.Raycast(player, dir, out hit) > 0)
                {
                    if (selectedOperation == OperationType.Destroy)
                    {
                        PenInteractable interactable = hit.collider.GetComponent<PenInteractable>();
                        if (interactable is not null)
                        {
                            selected = interactable;
                        }
                    }
                }
            }
        }

        public void Interact()
        {
            if (selected is not null && selected.InteractionPossible)
            {
                selected.TryInteract(hit.point, Quaternion.identity);
            }
        }
    }
}
