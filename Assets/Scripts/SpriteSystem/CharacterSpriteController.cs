using UnityEngine;

namespace SpriteSystem
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CharacterSpriteController : MonoBehaviour
    {
        public SpriteData spriteData; // Reference to the ScriptableObject
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void OnMovement(Vector2 deltaMove)
        {
            Direction direction = SpriteDirection.GetDirectionFromVector(deltaMove);
            UpdateSprite(direction);
        }

        public void UpdateSprite(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    SetSprite(spriteData.north, false);
                    break;
                case Direction.NorthEast:
                    SetSprite(spriteData.northEast, false);
                    break;
                case Direction.East:
                    SetSprite(spriteData.east, false);
                    break;
                case Direction.SouthEast:
                    SetSprite(spriteData.southEast, false);
                    break;
                case Direction.South:
                    SetSprite(spriteData.south, false);
                    break;
                case Direction.SouthWest:
                    SetSprite(spriteData.southWest, spriteData.useMirroringForSouthWest);
                    break;
                case Direction.West:
                    SetSprite(spriteData.west, spriteData.useMirroringForWest);
                    break;
                case Direction.NorthWest:
                    SetSprite(spriteData.northWest, spriteData.useMirroringForNorthWest);
                    break;
            }
        }

        private void SetSprite(Sprite sprite, bool useMirror)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.flipX = useMirror;
        }
    }
}