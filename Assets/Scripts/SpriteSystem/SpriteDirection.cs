using UnityEngine;

namespace SpriteSystem
{
    public static class SpriteDirection
    {
        public static Direction GetDirectionFromVector(Vector2 direction)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            if (angle < 0) angle += 360;

            if (angle >= 337.5f || angle < 22.5f) return Direction.East;
            if (angle >= 22.5f && angle < 67.5f) return Direction.NorthEast;
            if (angle >= 67.5f && angle < 112.5f) return Direction.North;
            if (angle >= 112.5f && angle < 157.5f) return Direction.NorthWest;
            if (angle >= 157.5f && angle < 202.5f) return Direction.West;
            if (angle >= 202.5f && angle < 247.5f) return Direction.SouthWest;
            if (angle >= 247.5f && angle < 292.5f) return Direction.South;
            if (angle >= 292.5f && angle < 337.5f) return Direction.SouthEast;

            return Direction.South;
        }
    }
}