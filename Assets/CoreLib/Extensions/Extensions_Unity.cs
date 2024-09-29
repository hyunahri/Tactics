using System.Collections.Generic;
using UnityEngine;

namespace CoreLib.Extensions
{
    public static class Extensions_Unity
    {
        public static Vector3 SumVectors(this List<Vector3> vecs)
        {
            Vector3 output = new Vector3();
            foreach (var entry in vecs)
                output += entry;
            return output;
        }
        
        public static float GetDistanceTo(this MonoBehaviour source, MonoBehaviour targ) => Vector3.Distance(source.transform.position, targ.transform.position);
        public static float GetDistanceTo(this MonoBehaviour source, Transform targ) => Vector3.Distance(source.transform.position, targ.position);


        public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag)where T:Component{
            Transform t = parent.transform;
            foreach(Transform tr in t)
            {
                if(tr.CompareTag(tag))
                {
                    return tr.GetComponent<T>();
                }
            }
            return null;
        }

        public static GameObject FindChildWithTag(this GameObject parent, string tag)
        {
            Transform t = parent.transform;
            foreach(Transform tr in t)
            {
                if(tr.CompareTag(tag))
                {
                    return tr.gameObject;
                }
            }
            return null;
        }
        
        public static void SetLayerRecursively(this GameObject obj, int newLayer)
        {
            if (null == obj)
            {
                return;
            }
       
            obj.layer = newLayer;
       
            foreach (Transform child in obj.transform)
            {
                if (null == child)
                {
                    continue;
                }
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}