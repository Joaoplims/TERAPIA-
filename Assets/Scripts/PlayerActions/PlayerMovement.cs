using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

namespace Terapia
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement :MonoBehaviour
    {
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        [SerializeField]private float playerSpeed = 2.0f;
        private float jumpHeight = 1.0f;
        private float gravityValue = -9.81f;
        private Vector3 inputMoveVector;
        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>( );
        }

        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
    
            controller.Move(inputMoveVector * Time.deltaTime * playerSpeed);

            if (inputMoveVector != Vector3.zero)
            {
                gameObject.transform.forward = inputMoveVector;
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        }

        public void GetInputVector(CallbackContext ctx)
        {
            if (ctx.performed)
            {
                var input = ctx.ReadValue<Vector2>( );
                inputMoveVector.x = input.x;
                inputMoveVector.z = input.y;
            }
            else if(ctx.canceled) inputMoveVector = Vector3.zero;
        }
    }
}
