using UnityEngine;

namespace SpriteSystem
{
    [CreateAssetMenu(fileName = "SpriteData", menuName = "Character/SpriteData")]
    public class SpriteData : ScriptableObject
    {
        public Sprite north;
        public Sprite northEast;
        public Sprite east;
        public Sprite southEast;
        public Sprite south;
        public Sprite southWest;
        public Sprite west;
        public Sprite northWest;

        public bool useMirroringForWest; // Mirror east for west
        public bool useMirroringForSouthWest; // Mirror southEast for southWest
        public bool useMirroringForNorthWest; // Mirror northEast for northWest
    }
}