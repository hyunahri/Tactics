using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mono
{
        public class CameraController : MonoBehaviour
        {
            //Blocking
            private static HashSet<GameObject> Blocking = new HashSet<GameObject>();

            public static void Block(GameObject go) => Blocking.Add(go);
            public static void Unblock(GameObject go) => Blocking.Remove(go);

            //Camera
            private Camera cam;
            
            private bool isBlocked => Blocking.Any();

            public float panSpeed = 4.5f;
            public float zoomSpeed = 3f;
            public float momentumDecay = 0.9f;

            [Header("Boundaries")] 
            Vector2 minBoundaries = new Vector2(-10f, -10f);

            Vector2 maxBoundaries = new Vector2(500f, 400f);
            Vector2 zoomBoundaries = new Vector2(1f, 20f);

            private Vector2 currentPanVelocity;
            private float currentZoomVelocity;

            private Vector3 pendingMouseDelta;

            //Dragging
            private Vector3 lastMousePosition;
            float dragSpeed = 4f;


            private void Awake()
            {
                cam = GetComponent<Camera>();
                cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, zoomBoundaries.x, zoomBoundaries.y);
                Vector3 newPosition = transform.position;
                newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
                newPosition.z = Mathf.Clamp(newPosition.z, minBoundaries.y, maxBoundaries.y);
                transform.position = newPosition;
            }
            
            void LateUpdate()
            {
                if (Input.GetMouseButtonDown(2))
                {
                    lastMousePosition = Input.mousePosition;
                }
                else if (Input.GetMouseButton(2))
                {
                    pendingMouseDelta = Input.mousePosition - lastMousePosition;
                    lastMousePosition = Input.mousePosition;
                }
                else
                {
                    pendingMouseDelta = Vector3.zero;
                }

                //if(EventSystem.current.currentSelectedGameObject != null && EventSystem.current.currentSelectedGameObject.GetComponent<TMP_InputField>() != null)
                //    return;
                HandlePan();
                HandleZoom();
            }

            private void FixedUpdate()
            {
                if (pendingMouseDelta != Vector3.zero)
                {
                    Vector3 newPosition = transform.position - new Vector3(pendingMouseDelta.x, 0, pendingMouseDelta.y) * dragSpeed * Time.fixedDeltaTime;
                    newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
                    newPosition.z = Mathf.Clamp(newPosition.z, minBoundaries.y, maxBoundaries.y);
                    transform.position = newPosition;
                    //StaticEventsCampaign.OnCameraMove.Invoke();
                }
            }

            void HandlePan()
            {
                // Gather input
                float moveX = Input.GetAxis("Horizontal");
                float moveY = Input.GetAxis("Vertical");

                // Apply movement with momentum
                Vector2 direction = new Vector2(moveX, moveY);
                currentPanVelocity += direction * panSpeed * Time.deltaTime;
                currentPanVelocity *= momentumDecay;

                Vector3 newPosition = transform.position + new Vector3(currentPanVelocity.x, 0, currentPanVelocity.y);
                newPosition.x = Mathf.Clamp(newPosition.x, minBoundaries.x, maxBoundaries.x);
                newPosition.z = Mathf.Clamp(newPosition.z, minBoundaries.y, maxBoundaries.y);


                transform.position = newPosition;
                //if (currentPanVelocity != Vector2.zero)
                //    StaticEventsCampaign.OnCameraMove.Invoke();
            }

            void HandleZoom()
            {
                return;
                float scrollInput = /* isZoomBlocked ? 0 :*/ Input.GetAxis("Mouse ScrollWheel");

                // Apply zooming with momentum
                currentZoomVelocity += scrollInput * zoomSpeed;
                currentZoomVelocity *= momentumDecay;

                if (currentZoomVelocity == 0)
                    return;

                cam.fieldOfView += currentZoomVelocity;
                cam.fieldOfView = Mathf.Clamp(cam.orthographicSize, zoomBoundaries.x, zoomBoundaries.y);
                //StaticEventsCampaign.OnCameraZoom.Invoke(cam.orthographicSize);
                //StaticEventsCampaign.OnCameraMove.Invoke();
            }
        }
}