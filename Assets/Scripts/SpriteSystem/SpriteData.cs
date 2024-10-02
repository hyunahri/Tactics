using UnityEngine;

namespace SpriteSystem
{
    [CreateAssetMenu(fileName = "SpriteData", menuName = "Graphics/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        [System.Serializable]
        public class DirectionalAnimation
        {
            public Sprite[] sprites; // Sprites for the animation loop
            public float fps = 10f;  // Frames per second
        }

        public DirectionalAnimation north;
        public DirectionalAnimation northEast;
        public DirectionalAnimation east;
        public DirectionalAnimation southEast;
        public DirectionalAnimation south;


        public bool useMirroringForWest => true; // Mirror east for west
        public bool useMirroringForSouthWest => true; // Mirror southEast for southWest
        public bool useMirroringForNorthWest => true; // Mirror northEast for northWest
    }
}