using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CoreLib.Utilities
{
    public class ActivateOnEnter : MonoBehaviour, IPointerEnterHandler
    {

        public List<GameObject> ToActivate = new List<GameObject>();
        public List<GameObject> ToDeactivate = new List<GameObject>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            
            foreach (var go in ToActivate)
            {
                go.SetActive(true);
            }
            foreach (var go in ToDeactivate)
            {
                go.SetActive(false);
            }
        }
    }
}
