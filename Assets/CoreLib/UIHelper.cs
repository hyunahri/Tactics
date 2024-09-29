using UnityEngine;
using UnityEngine.UI;

namespace CoreLib
{
    public static class UIHelper
    {
        
        public static void RefreshLayoutGroupsImmediateAndRecursive(GameObject root)

        {

            var componentsInChildren = root.GetComponentsInChildren<LayoutGroup>(true);

            foreach (var layoutGroup in componentsInChildren)

            {

                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup.GetComponent<RectTransform>());

            }



            var parent = root.GetComponent<LayoutGroup>();

            LayoutRebuilder.ForceRebuildLayoutImmediate(parent.GetComponent<RectTransform>());

        }
    }
}