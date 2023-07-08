using UnityEngine;
using System.Collections;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private InputController inputController;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera mainCam;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float jumpStrenght;
        [SerializeField] private float maxJumpHeight;
        [SerializeField] private float gravityStrenght = 9.98f;
        private float camYRotation;
        private float gravityForce;
        private bool onGround;
        private Coroutine jumpCoroutine;

        private void Start() => inputController.inputActions.MovementMap.Jump.performed += _ => StartJump();

        private void Update()
        {
            onGround = Physics.Raycast(transform.position, -transform.up, (float)(characterController.height / 2) + 0.1f);
            Move(inputController.inputActions.MovementMap.Move.ReadValue<Vector2>());
            Rotation(inputController.inputActions.MovementMap.MouseRotation.ReadValue<Vector2>());
            if (jumpCoroutine == null) GravityFall();
        }

        private void Move(Vector2 inputVector)
        {
            Vector3 moveVector = new(inputVector.x, 0.0f, inputVector.y);
            moveVector = transform.TransformDirection(moveVector);
            characterController.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        private void Rotation(Vector2 mouseRotation)
        {
            transform.Rotate(new Vector3(0.0f, mouseRotation.x, 0.0f) * rotationSpeed * Time.deltaTime);
            camYRotation += -mouseRotation.y * rotationSpeed * Time.deltaTime;
            camYRotation = Mathf.Clamp(camYRotation, -65f, 65f);
            mainCam.transform.eulerAngles = new Vector3(camYRotation, transform.eulerAngles.y, 0.0f);
        }

        private void StartJump()
        {
            if (onGround) jumpCoroutine = StartCoroutine(Jumping());
        }

        private IEnumerator Jumping()
        {
            float targetJumpHeight = transform.position.y + maxJumpHeight;
            while (Mathf.Abs(targetJumpHeight - transform.position.y) > 0.15f)
            {
                characterController.Move(Vector3.up * jumpStrenght * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            jumpCoroutine = null;
        }

        private void GravityFall()
        {
            if (onGround) gravityForce = 0;
            else
            {
                gravityForce += -gravityStrenght * Time.deltaTime;
                characterController.Move(Vector3.up * gravityForce * Time.deltaTime);
            }
        }
    }
}