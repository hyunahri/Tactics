using UnityEngine;
using UnityEngine.InputSystem;

public class BlinkOnInputAction : MonoBehaviour
{
    [SerializeField] private float BlinkDuration = 0.3f;
    [SerializeField] private GameObject Target;
    [SerializeField] private InputActionReference actionReference; // Assignable in inspector
    private InputAction inputAction;

    private void Awake()
    {
        if (actionReference != null)
        {
            inputAction = actionReference.action;
        }
    }

    private void OnEnable()
    {
        if (inputAction == null) return;
        inputAction.Enable();
        inputAction.performed += OnPerformed;
    }

    private void OnDisable()
    {
        if (inputAction == null) return;
        inputAction.performed -= OnPerformed;
        inputAction.Disable();
    }

    private void OnPerformed(InputAction.CallbackContext obj)
    {
        Target.SetActive(false);
        Invoke(nameof(EnableTarget), BlinkDuration);
    }

    private void EnableTarget()
    {
        Target.SetActive(true);
    }
}