using System;
using CoreLib.Events;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClockVisualController : MonoBehaviour
    {
        [SerializeField] private RectTransform arm;
        [SerializeField] private DayNightCycle dayNightCycle;
        [SerializeField] private Outline armOutline;

        private float rotationAtNoon = 270f;   // 12:00 (noon) should be at 270 degrees
        private float rotationAtMidnight = 90f; // 00:00 (midnight) should be at 90 degrees

        [SerializeField] private float targetRotation;
        
        private void OnEnable()
        {
            StaticEvents_Time.OnTime.AddListener(OnTime);
        }

        private void OnDisable()
        {
            StaticEvents_Time.OnTime.RemoveListener(OnTime);
        }

        private void OnTime(float time)
        {
            // Convert time (0 - 24) into a normalized value (0 - 1)
            float normalizedTime = time / 24f;
            // Lerp the rotation angle across the full circle smoothly
            targetRotation = -normalizedTime * 360f + 90f;

            // Set the local rotation of the arm to the computed angle
            arm.localEulerAngles = new Vector3(0, 0, targetRotation);

            // Set the arm outline color based on the time of day
            Color32 color = dayNightCycle.dayNightColor.Evaluate(normalizedTime);
            armOutline.effectColor = color;
        }
    }

}