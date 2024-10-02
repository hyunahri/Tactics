using System;
using UnityEngine;
using World;

namespace Mono
{
    /// <summary>
    /// Detects when player is near an interactable object and adds it to the list of pending interactions
    /// </summary>
    public class PlayerInteractionGrabber : MonoBehaviour
    {
        public bool Debugging = true;
        private InputSystem_Actions input;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IWorldInteractable interactable) == false)
                return;
            if (Debugging) Debug.Log($"Player ENTERED {interactable.GetName()}");

            PlayerInteractionManager.AddPendingInteraction(interactable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out IWorldInteractable interactable) == false)
                return;
            if (Debugging) Debug.Log($"Player EXITED {interactable.GetName()}");

            PlayerInteractionManager.RemovePendingInteraction(interactable);
        }

        private void Awake()
        {
            input = new InputSystem_Actions();
            //on tapped interact
            input.Player.Interact.performed += _ => TryInteract();
        }

        private void OnEnable()
        {
            input.Player.Enable();
        }

        private void OnDisable()
        {
            input.Player.Disable();
        }

        private void TryInteract()
        {
            if (Debugging)
                Debug.Log("Player tapped interact");
            var interaction = PlayerInteractionManager.PriorityInteraction;
            if (interaction != null && !PlayerInteractionManager.SuppressInteractions)
            {
                if (Debugging)
                    Debug.Log($"Player interacted with {interaction.Interactable.GetName()}");
                interaction.Interactable.Interact();
            }
        }
    }
}