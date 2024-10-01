using UnityEngine.ProBuilder.Shapes;

namespace World
{
    public interface IWorldInteractable
    {
        public string GetName();
        public Sprite GetInteractionIcon();
        public bool CanInteract();
        public void Interact();
    }
}