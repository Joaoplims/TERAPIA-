using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Pill;
using static UnityEngine.InputSystem.InputAction;

namespace Terapia
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerActions :MonoBehaviour, IEnableInput
    {
        public bool LockInput { get; set; }
        public bool InvertControlls { get; set; }

        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private PlayerStates currentState = PlayerStates.Walking;
        [SerializeField] private PlayerHudController hudManager;

        private float staminaAmmout = 1;
        private float maxStamina = 1f;
        private CharacterController controller;
        private Vector3 inputMoveVector;
        private bool enableStaminaRecovery = false;
        private void Start()
        {
            controller = gameObject.GetComponent<CharacterController>( );
            LockInput = false;
        }

        void Update()
        {
            if (LockInput == true)
                return;

            if (Keyboard.current.leftShiftKey.isPressed && inputMoveVector != Vector3.zero)
            {
                currentState = PlayerStates.Running;
                staminaAmmout -= 0.01f;
                hudManager.SetStaminaValue(staminaAmmout);
                if (staminaAmmout <= 0f)
                {
                    currentState = PlayerStates.Walking;
                    staminaAmmout = 0f;
                }
            }
            else if (Keyboard.current.leftShiftKey.wasReleasedThisFrame)
            {
                currentState = PlayerStates.Walking;
                enableStaminaRecovery = true;
            }
            RecoverStamina( );

            float speed = currentState == PlayerStates.Walking ? playerSpeed : ( playerSpeed * 2f );
            controller.Move(inputMoveVector * Time.deltaTime * speed);

            if (inputMoveVector != Vector3.zero)
            {
                gameObject.transform.forward = inputMoveVector;
            }

        }

        public void GetInputVector(CallbackContext ctx)
        {
            if (LockInput == true)
                return;

            if (ctx.performed)
            {
                var input = ctx.ReadValue<Vector2>( );
                inputMoveVector.x = InvertControlls == true ? -input.x : input.x;
                inputMoveVector.z = InvertControlls == true ? -input.y : input.y;
            }
            else if (ctx.canceled)
                inputMoveVector = Vector3.zero;
        }
        public void SetDebuff(DebuffTypes debuffType)
        {
            switch (debuffType)
            {
                case DebuffTypes.LooseBreath:
                maxStamina = 0.5f;
                hudManager.SetStaminaValue(staminaAmmout);
                hudManager.SetMaxStamina(0.5f);
                this.Invoke(() =>
                {
                    maxStamina = 1f;
                    hudManager.SetStaminaValue(staminaAmmout);
                    hudManager.SetMaxStamina(1f);
                } , 8f);
                break;
                case DebuffTypes.InvertControll:
                InvertControlls = true;
                this.Invoke(() => InvertControlls = false , 2f);
                break;
                case DebuffTypes.ScreenShake:
                this.Invoke(() => CameraShake.instance.Shake(15f) , 1f);
                break;
                default:
                break;
            }
        }
        public void EndGame()
        {
            LockInput = true;
            hudManager.ShowEndGamePanel( );
        }

        private void RecoverStamina()
        {
            if (enableStaminaRecovery == true)
            {
                staminaAmmout += 0.001f;

                if (staminaAmmout >= maxStamina)
                {

                    staminaAmmout = maxStamina;
                    enableStaminaRecovery = false;
                }
                hudManager.SetStaminaValue(staminaAmmout);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
            
                LockInput = true;
                hudManager.ShowEndGamePanel( );
                other.GetComponent<IEnableInput>( ).LockInput = true;
            }
        }

        public enum PlayerStates
        {
            Walking,
            Running
        }
    }
}
