using Events;
using SpriteSystem;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CharacterSpriteController spriteController;

    [Header("State")] 
    public bool IsRunning = false;

    [Header("Settings")]
    [SerializeField] private float moveSpeedWalking = 5f;
    [SerializeField] private float moveSpeedRunning = 9f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;

    private Vector2 moveInput;
    private bool isInteractPressed;
    private bool isGrounded;
    
    private Vector3 velocity;
    private Transform groundCheck;

    private void Start()
    {
        groundCheck = new GameObject("GroundCheck").transform; // Create a ground check object
        groundCheck.parent = transform;
        groundCheck.localPosition = new Vector3(0, -characterController.height / 2, 0); // Position it at the bottom of the character
    }

    private void OnEnable()
    {
        var playerInput = new InputSystem_Actions();
        playerInput.Player.Enable();

        playerInput.Player.Move.performed += OnMovePerformed;
        playerInput.Player.Move.canceled += OnMoveCanceled;
        playerInput.Player.Sprint.performed += OnSprintPerformed;
        playerInput.Player.Sprint.canceled += OnSprintCanceled;
    }

    private void OnDisable()
    {
        var playerInput = new InputSystem_Actions();
        playerInput.Player.Disable();

        playerInput.Player.Move.performed -= OnMovePerformed;
        playerInput.Player.Move.canceled -= OnMoveCanceled;
        playerInput.Player.Sprint.performed -= OnSprintPerformed;
        playerInput.Player.Sprint.canceled -= OnSprintCanceled;
    }

    private void Update()
    {
        // Check if the player is grounded by performing a sphere cast at the player's feet
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset the downward velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep player "stuck" to the ground
        }

        // Determine whether the player is running or walking
        IsRunning = isInteractPressed;
        float speed = IsRunning ? moveSpeedRunning : moveSpeedWalking;

        // Calculate movement based on input and apply speed
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        characterController.Move(moveDirection * speed * Time.deltaTime);

        spriteController?.OnMovement(moveDirection);
        
        // Apply gravity
        velocity.y += gravity * Time.deltaTime;

        // Apply gravity movement
        characterController.Move(velocity * Time.deltaTime);
        if(moveDirection != Vector3.zero)
            GameEvents.OnPlayerMove.Invoke();
    }

    // Called when the Move action is performed
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Called when the Move action is canceled
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }

    // Called when the Interact action is performed (held down)
    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        isInteractPressed = true;
    }

    // Called when the Interact action is canceled (released)
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        isInteractPressed = false;
    }
}
