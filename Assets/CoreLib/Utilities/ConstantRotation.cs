using UnityEngine;

namespace CoreLib.Utilities
{
    public class ConstantRotation : MonoBehaviour
    {
        [SerializeField]private Vector3 eulerRot;
        void Start()
        {
        
        }

        void Update()
        {
            if (gameObject.activeSelf == false)
                return;
            transform.Rotate(eulerRot * UnityEngine.Time.deltaTime);
        }
    }
}
