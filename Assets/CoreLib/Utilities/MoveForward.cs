using UnityEngine;

namespace CoreLib.Utilities
{
    public class MoveForward : MonoBehaviour
    {
        [SerializeField] private float Speed = 3f;
        private Camera cam;

        private void Start()
        {
            cam = Camera.main;
        }

        private void Update()
        {
            transform.position += transform.forward * UnityEngine.Time.deltaTime * Speed;
            if (cam.fieldOfView < 70)
                cam.fieldOfView += UnityEngine.Time.deltaTime * 1f;
        }
    }
}
