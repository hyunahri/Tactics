using UnityEngine;

namespace CoreLib.Utilities
{
    public class CursorSetup : MonoBehaviour
    {
        [SerializeField] private Sprite CursorSprite;
        [SerializeField] private Sprite ClickedSprite;
    
        void Awake()
        {
            Cursor.SetCursor(CursorSprite.texture, Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(ClickedSprite.texture, Vector2.zero, CursorMode.Auto);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                Cursor.SetCursor(CursorSprite.texture, Vector2.zero, CursorMode.Auto);
            }
        }
    }
}
