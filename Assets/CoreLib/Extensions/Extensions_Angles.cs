using UnityEngine;

namespace CoreLib.Extensions
{
    public static class Extensions_Angles
    {
        
        public static bool IsBetweenAngles(this float angle, float angleRange) => angle >= -angleRange && angle <= angleRange;

        public static float GetAngleTo(this MonoBehaviour source, Transform targ)
        {
            Vector3 targetDir = targ.position - source.transform.position;
            return Vector3.Angle(targetDir, source.transform.forward);
        }
        
        public static float GetAngleTo(this Transform source, Transform targ)
        {
            Vector3 targetDir = targ.position - source.position;
            return Vector3.Angle(targetDir, source.forward);
        }
    }
}