using UnityEngine;

namespace Controller
{
    public class InputController : MonoBehaviour
    {
        public InputMap inputActions;

        private void Awake()
        {
            if (inputActions == null) inputActions = new();
        }

        private void OnEnable()
        {
            inputActions.Enable();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable() => inputActions.Disable();
    }
}