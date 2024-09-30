using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Tooltips
{
    /// <summary>
    /// Place this on any UI element that you want to show a tooltip for.
    /// </summary>
    public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Tooltip")]
        [TextArea(3, 10)]
        [SerializeField] private string tooltipText;
        public string TooltipText
        {
            get => tooltipText;
            set => tooltipText = value;
        }

        [Header("Settings")]
        [SerializeField] private bool onlyInHelpMode = false;
        public bool OnlyInHelpMode => onlyInHelpMode;
        
        [SerializeField] private float TooltipDelay = 1f;
        [SerializeField] public Color32 TooltipColor = new Color32(255, 255, 255, 255);
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipReceiver.Instance?.AddTooltip(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipReceiver.Instance?.RemoveTooltip(this);
        }

        private void OnDisable()
        {
            TooltipReceiver.Instance?.RemoveTooltip(this);
        }
    }
}