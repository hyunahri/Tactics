using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames
{
    public class GryphonMovement : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private AudioSource rollSource;

        public Transform PlayerBodyTransform;
        public CharacterController controller;
        public Transform GroundCheckTransform;

        public const float GRAVITY = -20f;

        public float speed => isSprinting ? 20f : 14f;

        [Header("Settings")]
        public float GroundDistance;
        public LayerMask GroundMask;
        public float jumpHeight = 3f;

        [Header("State")]
        [SerializeField] public Vector3 inputVector;
        [SerializeField] public Vector3 playerVelocity;
        [SerializeField] public Vector3 cachedGravity;
        [SerializeField] public Vector3 rollVelocity;

        [SerializeField] public bool isGrounded;
        [SerializeField] public bool isRolling;

        public bool isSprinting = false;

        // Input actions
        private InputAction moveAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        private InputAction rollAction;

        private void OnEnable()
        {
            // Set up input actions
            moveAction = new InputAction("Move", binding: "<Gamepad>/leftStick");
            jumpAction = new InputAction("Jump", binding: "<Gamepad>/buttonSouth");
            sprintAction = new InputAction("Sprint", binding: "<Gamepad>/buttonEast");
            rollAction = new InputAction("Roll", binding: "<Gamepad>/buttonWest");

            moveAction.AddCompositeBinding("Dpad")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");

            moveAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
            rollAction.Enable();
        }

        private void OnDisable()
        {
            // Disable input actions when not needed
            moveAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
            rollAction.Disable();
        }

        private void Update()
        {
            // Handle ground check
            isGrounded = Physics.CheckSphere(GroundCheckTransform.position, GroundDistance, GroundMask);
            isGrounded = true;
            // Handle sprinting
            isSprinting = sprintAction.ReadValue<float>() > 0;

            // Handle rolling
            if (rollAction.triggered)
            {
                DoRoll();
            }

            // Get movement input
            Vector2 input = moveAction.ReadValue<Vector2>();
            inputVector = new Vector3(input.x, 0, input.y);

            // Calculate movement direction
            Vector3 move = PlayerBodyTransform.right * input.x + PlayerBodyTransform.forward * input.y;
            playerVelocity = move * speed;
            playerVelocity += rollVelocity;
            controller.Move(playerVelocity * Time.deltaTime);

            // Handle jumping
            if (jumpAction.triggered && isGrounded)
            {
                cachedGravity.y = Mathf.Sqrt(jumpHeight * -2f * GRAVITY);
            }

            // Apply gravity
            cachedGravity.y += GRAVITY * Time.deltaTime;
            controller.Move(cachedGravity * Time.deltaTime);

            // Reset gravity when grounded
            if (controller.isGrounded && cachedGravity.y < 0)
            {
                cachedGravity.y = -0.1f;
            }
        }

        private void DoRoll()
        {
            if (inputVector == Vector3.zero)
                return;
            if (!isGrounded || isRolling)
                return;

            rollSource?.Play();
            StartCoroutine(Rolling());
        }

        IEnumerator Rolling()
        {
            isRolling = true;
            float rollTime = 0.5f;
            float rollSpeedMultiplier = .33f;
            float rollSpeed = speed * rollSpeedMultiplier;
            Vector3 rollDirection = PlayerBodyTransform.forward * inputVector.z + PlayerBodyTransform.right * inputVector.x;
            rollVelocity = rollDirection * rollSpeed;
            float timer = 0f;
            float midpoint = rollTime / 2f;
            float scaleAtMidpoint = 0.33f;

            while (timer < rollTime)
            {
                timer += Time.deltaTime;
                //reduce scale to 0.5 by midpoint
                if (timer < midpoint)
                {
                    //reduce scale
                    transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * scaleAtMidpoint, timer / midpoint);
                }
                else
                {
                    //increase scale
                    transform.localScale = Vector3.Lerp(Vector3.one * scaleAtMidpoint, Vector3.one, (timer - midpoint) / midpoint);
                }

                yield return null;
            }

            rollVelocity = Vector3.zero;
            transform.localScale = Vector3.one;
            isRolling = false;
        }
    }
}
