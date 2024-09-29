using UnityEngine;
using UnityEngine.UI;

namespace CoreLib.Extensions
{
    public static class Extensions_UI
    {
        public static Color32 ToDim(this Color32 color) => new Color32(color.r, color.g, color.b, (byte)(color.a - 60));
        public static Color32 ToBright(this Color32 color) => new Color32(color.r, color.g, color.b, 255);
        [Tooltip("Includes # already")]
        public static string ToHTML(this Color32 color) => $"#{ColorUtility.ToHtmlStringRGBA(color)}";

        public static void ForceRebuildLayout(this RectTransform rectTransform) => LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        public static void ForceRebuildLayout(this GameObject gameObject) => LayoutRebuilder.ForceRebuildLayoutImmediate(gameObject.GetComponent<RectTransform>());
        public static void ForceRebuildLayout(this Transform tr)
        {
            //Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(tr.GetComponent<RectTransform>());
            // LayoutRebuilder.ForceRebuildLayoutImmediate(tr.GetComponent<RectTransform>());
        }
    }
}