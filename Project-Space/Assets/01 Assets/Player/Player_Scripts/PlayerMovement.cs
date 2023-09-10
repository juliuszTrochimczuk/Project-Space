using System.Collections;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Inject] private Controller.InputController inputController;

        [Header("Components")]
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Camera mainCam;

        [Header("Basic Movement parametrs")]
        [SerializeField] private float normalMoveSpeed;
        [SerializeField] private float crounchMoveSpeed;
        private float moveSpeed;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private Vector3 cameraNormalOffset;

        [Header("Jump parametrs")]
        [SerializeField] private float jumpStrenght;
        [SerializeField] private float maxJumpHeight;

        [Space(20)]
        [SerializeField] private float gravityStrenght = 9.98f;

        [Header("Crounching parametrs")]
        [SerializeField] private float playerCrounchingHeight;
        [SerializeField] private Vector3 cameraCrounchingOffset;
        
        private float camYRotation;
        private float gravityForce;
        private float playerNormalHeight;
        private bool onGround;
        private Coroutine jumpCoroutine;

        private void Start()
        {
            inputController.inputActions.MovementMap.Jump.performed += _ => StartJump();
            inputController.inputActions.MovementMap.Crouch.started += _ => StartCrounching();
            inputController.inputActions.MovementMap.Crouch.canceled += _ => EndCrounching();

            playerNormalHeight = characterController.height;
            moveSpeed = normalMoveSpeed;
        }

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
            camYRotation = Mathf.Clamp(camYRotation, -80f, 80f);
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

        private void StartCrounching()
        {
            characterController.height = playerCrounchingHeight;
            moveSpeed = crounchMoveSpeed;
            mainCam.transform.position = gameObject.transform.position + cameraCrounchingOffset;
        }

        private void EndCrounching()
        {
            characterController.height = playerNormalHeight;
            moveSpeed = normalMoveSpeed;
            gameObject.transform.position += new Vector3(0f, 1f, 0f);
            mainCam.transform.position = gameObject.transform.position + cameraNormalOffset;
        }
    }
}