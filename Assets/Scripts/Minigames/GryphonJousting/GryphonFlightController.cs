using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GryphonFlightController : MonoBehaviour
{
    [SerializeField] private LazyBar staminaBar;
    public Rigidbody rb;
    public float flapForce = 10f;
    public float normalFallSpeed = 2f;  // Increase to make descent more noticeable
    public float plummetFallSpeed = 10f; // Increase plummet speed for a stronger downward pull
    public float horizontalSpeed = 5f;
    public float maxVerticalSpeed = 20f; // Cap for the maximum vertical speed (both up and down)
    public float horizontalSpeedSoftCap = 10f;  // The soft cap for horizontal speed
    public float dragMultiplier = 0.1f;         // Drag multiplier for speed exceeding the cap
    
    [FormerlySerializedAs("isPlummeting")] public bool IsPlummeting = false;
    private bool canFlap = true;
    
    private InputSystem_Actions inputActions;
    public Vector2 movementInput;

    public bool IsSprinting = false;
    
    public float Stamina = 100f;
    public float MaxStamina = 100f;
    public float StaminaRegenRate = 10f;
    public float FlapStaminaCost = 8f;
    
    //Drag
    private Vector3 horizontalVelocity;

    public float xDragMultiplier = 0.2f;        // Higher drag in the local X direction
    public float zDragMultiplier = 0.1f;        // Lower drag in the local Z direction
    public float fallSpeedToForwardMultiplier = 0.5f; // How much falling increases forward speed
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputActions = new InputSystem_Actions();
        
        // Bind actions
        inputActions.Gryphon.Flap.performed += _ => Flap();
        inputActions.Gryphon.Plummet.performed += _ => TogglePlummet();
        inputActions.Gryphon.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Gryphon.Move.canceled += _ => movementInput = Vector2.zero;
    }

    private void OnEnable()
    {
        inputActions.Gryphon.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gryphon.Disable();
    }

    private void FixedUpdate()
    {
        IsPlummeting = inputActions.Gryphon.Plummet.IsPressed();
        Stamina = Mathf.Clamp(Stamina + StaminaRegenRate * Time.fixedDeltaTime, 0, MaxStamina);

        HandleFlight();
        HandleMovement();

        // Cap the vertical velocity to avoid floatiness
        CapVerticalVelocity();
    }

    private void Update()
    {
        if(staminaBar)
            staminaBar.Set(Stamina / MaxStamina);
    }
    
    private void HandleFlight()
    {
        if (IsPlummeting)
        {
            // Increase downward force for plummeting
            rb.AddForce(Vector3.down * plummetFallSpeed, ForceMode.Acceleration);
        }
        else
        {
            // Normal flight, slowly losing altitude
            rb.AddForce(Vector3.down * normalFallSpeed, ForceMode.Acceleration);
        }
    }


private void HandleMovement()
{
    // Get the current vertical velocity
    float verticalVelocity = rb.linearVelocity.y;

    // Convert movement input to gryphon local space
    Vector3 worldInputDirection = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y)) * horizontalSpeed;

    // Transfer energy from falling to forward
    float forwardSpeedBoost = -Mathf.Clamp(verticalVelocity, -maxVerticalSpeed, 0f) * fallSpeedToForwardMultiplier;

    // Apply the forward boost in Z
    Vector3 moveDirection = new Vector3(worldInputDirection.x, 0, worldInputDirection.z + forwardSpeedBoost);

    // Apply inertia to smooth sideways movement XZ
    horizontalVelocity = Vector3.Lerp(horizontalVelocity, moveDirection, Time.fixedDeltaTime * 2f);

    float currentHorizontalSpeedXZ = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;

    // Apply drag based on excess speed over the soft cap, with different drag for X and Z directions
    if (currentHorizontalSpeedXZ > horizontalSpeedSoftCap)
    {
        // Apply higher drag in the X direction and lower in the Z direction
        float excessSpeed = currentHorizontalSpeedXZ - horizontalSpeedSoftCap;

        // Apply drag forces to X and Z
        float xDragForce = excessSpeed * xDragMultiplier;
        float zDragForce = excessSpeed * zDragMultiplier;

        // Reduce horizontal velocity based on drag forces
        horizontalVelocity.x *= (1f - xDragForce * Time.fixedDeltaTime);
        horizontalVelocity.z *= (1f - zDragForce * Time.fixedDeltaTime);
    }

    // Apply the updated horizontal velocity to the rigidbody
    rb.linearVelocity = new Vector3(horizontalVelocity.x, rb.linearVelocity.y, horizontalVelocity.z);
}



    private void Flap()
    {
        
        if (Stamina < FlapStaminaCost || !canFlap || IsPlummeting)
        {
            Debug.Log("Failed to flap");
            return;
        }
        Debug.Log("Flap");

        // Apply a strong upward force when flapping
        rb.AddForce(Vector3.up * flapForce, ForceMode.VelocityChange);
        canFlap = false;
        Stamina -= FlapStaminaCost;
        // Cooldown for flapping
        Invoke(nameof(ResetFlap), 0.33f); 
    }

    private void ResetFlap()
    {
        canFlap = true;
    }

    private void TogglePlummet()
    {
        IsPlummeting = inputActions.Gryphon.Plummet.IsPressed();
    }

    private void CapVerticalVelocity()
    {
        // Clamp the vertical velocity to prevent excessive floating or falling speed
        if (Mathf.Abs(rb.linearVelocity.y) > maxVerticalSpeed)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sign(rb.linearVelocity.y) * maxVerticalSpeed, rb.linearVelocity.z);
        }
    }
}
