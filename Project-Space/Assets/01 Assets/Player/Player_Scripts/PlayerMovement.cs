using UnityEngine;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    [Inject] private InputController inputController;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private float camYRotation;

    private void Update()
    {
        Vector2 inputVector = inputController.inputActions.MovementMap.Move.ReadValue<Vector2>();
        Vector3 moveVector = new(inputVector.x, 0.0f, inputVector.y);
        moveVector = transform.TransformDirection(moveVector);
        characterController.Move(moveVector * moveSpeed * Time.deltaTime);

        Vector2 mouseRotation = inputController.inputActions.MovementMap.MouseRotation.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0.0f, mouseRotation.x, 0.0f) * rotationSpeed * Time.deltaTime);
        camYRotation += -mouseRotation.y * rotationSpeed * Time.deltaTime;
        camYRotation = Mathf.Clamp(camYRotation, -65f, 65f);
        mainCam.transform.eulerAngles = new Vector3(camYRotation, transform.eulerAngles.y, 0.0f);
    }
}