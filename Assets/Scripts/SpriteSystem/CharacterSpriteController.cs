using SpriteSystem;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CharacterSpriteController : MonoBehaviour
{
    public SpriteData spriteData; // Reference to the ScriptableObject

    private SpriteRenderer spriteRenderer;

    private Direction currentDirection = Direction.South; // Default direction
    private int currentFrame = 0;
    private float animationTimer = 0f;
    
    private SpriteData.DirectionalAnimation currentAnimation;
    private bool isMirrored = false;
    private bool isMoving = false; // Track if the player is moving

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetDirection(currentDirection, false); // Initialize with the default direction
    }

    private void Update()
    {
        return;
        if (isMoving)
        {
            // Animate when moving
        }
        else
        {
            // Set the first frame of the current animation (idle frame) when not moving
            if (currentAnimation != null && currentAnimation.sprites.Length > 0)
            {
                spriteRenderer.sprite = currentAnimation.sprites[0]; // Set to idle frame (first frame)
                spriteRenderer.flipX = isMirrored;
            }
        }
    }
    
    public void OnMovement(Vector3 movement)
    {
        if (movement == Vector3.zero)
        {
            SetDirection(currentDirection, false);
            return;
        }
        else
        {
            SetDirection(SpriteDirection.GetDirectionFromVector(movement), true);
        }
    }

    // Set the direction and switch to the corresponding animation
    public void SetDirection(Direction direction, bool moving)
    {
        isMoving = moving; // Update the movement state
        
        // Only change direction and animation if there's movement
        if (currentDirection != direction || !isMoving)
        {
            currentDirection = direction;
            currentFrame = 0; // Reset the frame

            switch (currentDirection)
            {
                case Direction.North:
                    SetAnimation(spriteData.north, false);
                    break;
                case Direction.NorthEast:
                    SetAnimation(spriteData.northEast, false);
                    break;
                case Direction.East:
                    SetAnimation(spriteData.east, false);
                    break;
                case Direction.SouthEast:
                    SetAnimation(spriteData.southEast, false);
                    break;
                case Direction.South:
                    SetAnimation(spriteData.south, false);
                    break;
                case Direction.SouthWest:
                    SetAnimation(spriteData.southEast, spriteData.useMirroringForSouthWest);
                    break;
                case Direction.West:
                    SetAnimation(spriteData.east, spriteData.useMirroringForWest);
                    break;
                case Direction.NorthWest:
                    SetAnimation(spriteData.northEast, spriteData.useMirroringForNorthWest);
                    break;
            }
        }
        else
        {
            AnimateSprite();
        }
    }

    private void SetAnimation(SpriteData.DirectionalAnimation animation, bool mirror)
    {
        currentAnimation = animation;
        currentFrame = 0; // Start at the first frame
        animationTimer = 0f; // Reset the timer
        isMirrored = mirror;

        // Set the sprite for the initial frame
        spriteRenderer.sprite = currentAnimation.sprites[currentFrame];
        spriteRenderer.flipX = isMirrored;
    }

    private void AnimateSprite()
    {
        if (currentAnimation == null || currentAnimation.sprites.Length == 0)
            return;

        // Increment the timer based on the frame rate
        animationTimer += Time.deltaTime;

        if (animationTimer >= 1f / currentAnimation.fps)
        {
            // Move to the next frame in the animation
            currentFrame = (currentFrame + 1) % currentAnimation.sprites.Length;
            spriteRenderer.sprite = currentAnimation.sprites[currentFrame];
            spriteRenderer.flipX = isMirrored;

            // Reset the timer for the next frame
            animationTimer = 0f;
        }
    }
}
