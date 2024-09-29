using UnityEngine;

namespace CoreLib.Utilities
{
    public class RigidbodyDebugger : MonoBehaviour
    {
        private Rigidbody RB;

        [SerializeField] private float Magnitude;
        [SerializeField] private Vector3 Velocity;
    
    
    
        private void Start()
        {
            RB = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!RB)
                return;
            Magnitude = RB.linearVelocity.magnitude;
            Velocity = RB.linearVelocity;
        }
    }
}
