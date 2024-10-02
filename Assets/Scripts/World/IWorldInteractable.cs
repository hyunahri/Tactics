using UnityEngine;
using Sprite = UnityEngine.ProBuilder.Shapes.Sprite;

namespace World
{
    public interface IWorldInteractable 
    {
        public string GetName();
        public Sprite GetInteractionIcon();
        public bool CanInteract();
        public void Interact();
        public void SetAsPriorityInteraction(bool isPriority);
        public Vector3 GetInteractionPosition();
        
        public void OnPlayerEntersInteractionRange();
        public void OnPlayerExitsInteractionRange();
    }
}