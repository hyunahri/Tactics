using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Minigames
{
    public class StickLook : MonoBehaviour
    {
        public Rigidbody rb;
        [SerializeField] public GryphonFlightController Movement;
        public Camera cam;
        public Transform PlayerBodyTransform;
        public Transform CameraTiltTransform;
        public Transform CameraRollTransform;

        public float RotationSensitivity = 20f;
        
        private const float MaxTilt = 75f;
        private const float MinTilt = -85f;
        private const float MaxRoll = 15f;
        private const float MinRoll = -15f;

        public float LookUpLimit = 25f;

        public bool TiltOnPlummet = true;

        [Header("FOV")] private float fov;
        private float averageFov = 60;
        private float fovOffsetAtMaxSpeed = 15;

        [Header("Rotations")] float tiltRot, rollRot, targetRoll;

        // InputAction for capturing mouse movement

        private InputSystem_Actions inputActions;
        
        private Vector2 lookInput;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputActions = new InputSystem_Actions();

            inputActions.Gryphon.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            inputActions.Gryphon.Look.canceled += _ => lookInput = Vector2.zero;
        }

        private void OnEnable()
        {
            // Set up the look action from the new Input System
            inputActions.Gryphon.Enable();

        }

        private void OnDisable()
        {
            inputActions.Gryphon.Disable();
        }

        private void Start()
        {
            fov = cam.fieldOfView;
        }

        private void Update()
        {
            // Get mouse input from the new Input System
            //Vector2 mouseDelta = lookAction.ReadValue<Vector2>();
            float mouseX = lookInput.x * RotationSensitivity * Time.deltaTime;
            float mouseY = lookInput.y * RotationSensitivity * Time.deltaTime;

            if (Movement.IsPlummeting)
            {
                WhilePlummeting();
            }
            else
            {

                // Rotate the camera based on mouse input
                tiltRot -= mouseY;
                tiltRot = Mathf.Clamp(tiltRot, MinTilt, MaxTilt);
                //clamp the tilt to the look up limit
                tiltRot = Mathf.Max(tiltRot, -LookUpLimit);

                CameraTiltTransform.localRotation = Quaternion.Euler(tiltRot, 0f, 0f);
                PlayerBodyTransform.Rotate(Vector3.up * mouseX);
            }

            AdjustFoV();
            AdjustRoll();
        }
        
        private void WhilePlummeting()
        {
            if (!TiltOnPlummet)
                return;
            
            // Smoothly interpolate the current tilt rotation towards the target tilt rotation
            float targetTilt = 0f;
            //target tilt is based on the current y velocity, max out at -50
            targetTilt = Mathf.Lerp(0, -20, Mathf.Abs(rb.linearVelocity.y) / 20);
            //but never tilt back up
            targetTilt = Mathf.Max(targetTilt, tiltRot);
            
            tiltRot = Mathf.Lerp(tiltRot, MaxTilt, Time.deltaTime * 2);
            CameraTiltTransform.localRotation = Quaternion.Euler(tiltRot, 0f, 0f);
        }

        void AdjustRoll()
        {
            // Smoothly yaw the transform based on x speed
            float xInput = Movement.movementInput.x;

            // Calculate the target yaw rotation based on x speed
            targetRoll = Mathf.Lerp(MaxRoll, MinRoll, (xInput + 1) / 2f);
            
            //go even further if also trying to look in that direction
            targetRoll += -lookInput.x * 10;

            // Smoothly interpolate the current yaw rotation towards the target yaw rotation
            rollRot = Mathf.Lerp(rollRot, targetRoll, Time.deltaTime * 2);
            CameraRollTransform.localRotation = Quaternion.Euler(0f, 0f, rollRot);
        }

        void AdjustFoV()
        {
            // Move FoV towards average FoV - offset if sprinting
            float targetFov = Movement.IsSprinting ? averageFov + fovOffsetAtMaxSpeed : averageFov;

            // Smoothly interpolate the current FoV towards the target FoV
            fov = Mathf.Lerp(fov, targetFov, Time.deltaTime * 2);
            cam.fieldOfView = fov;
        }
    }
}
