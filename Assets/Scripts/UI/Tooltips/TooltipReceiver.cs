using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tooltips
{
    /// <summary>
    /// Singleton for the tooltip system. This is the receiver for all tooltips in the game.
    /// </summary>
 public class TooltipReceiver : MonoBehaviour
    {
        public static TooltipReceiver Instance;
        
        private List<Tooltip> ActiveTooltips = new List<Tooltip>();

        [Header("Positioning - If this isn't lining up, make sure the anchor is set to bottom left and 0,0")]
        public Vector3 Offset = new Vector3(36, 24, 0);
        private Vector3 InverseOffset;
        
        [Header("Settings")] 
        public bool HelpMode = false;
        
        [Header("References")]
        public RectTransform TooltipParentRect;

        private RectTransform TooltipBackgroundRect;
        public Image TooltipBackground;
        
        private RectTransform TooltipTextRect;
        public TextMeshProUGUI TooltipText;
        
        public Camera Cam;

        private bool isLeftSide = false;
        
        private void Awake()
        {
            Instance = this;
            Cam = Camera.main;
            
            TooltipBackgroundRect = TooltipBackground.GetComponent<RectTransform>();
            TooltipTextRect = TooltipText.GetComponent<RectTransform>();
            RebuildTooltip();
            InverseOffset = new Vector3(-Offset.x, Offset.y, 0);
        }
        
        public void AddTooltip(Tooltip tooltip)
        {
            ActiveTooltips.Add(tooltip);
            RebuildTooltip();
        }
        
        public void RemoveTooltip(Tooltip tooltip)
        {
            ActiveTooltips.Remove(tooltip);
            RebuildTooltip();
        }
        
        private void RebuildTooltip()
        {
            Clear();
            TooltipBackgroundRect.pivot = new Vector2(Input.mousePosition.x > Screen.width / 2f ? 1 : 0, 1);
            foreach(var tooltip in ActiveTooltips)
            {
                if(HelpMode || !tooltip.OnlyInHelpMode)
                    TooltipText.text += tooltip.TooltipText + "\n";
            }

        }
        
        private void Clear()
        {
            TooltipText.text = "";
        }

        private void Update()
        {
            if (Cam == null || Cam.enabled == false)
                Cam = Camera.current;
            
            //snap to mouse
            Vector3 cursorWidth = new Vector3(64f, 0f, 0f);
            Vector3 mousePos = Input.mousePosition;
            TooltipParentRect.anchoredPosition = mousePos + (isLeftSide ? Offset + cursorWidth : InverseOffset);
            TooltipBackgroundRect.sizeDelta = TooltipTextRect.sizeDelta;
            //set pivot based on whether mouse is on left or right side of screen
        }
    }
}