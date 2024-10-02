using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{


    public class LazyBar : MonoBehaviour
    {
        public Image instantFillBar;  // The image that instantly moves to the result value
        public Image lazyFillBar;     // The image that waits a moment and then moves to the result value

        public float lazySpeed = 2f;  // Speed at which the lazy bar catches up
        public float delay = 0.33f;    // Delay before the lazy bar starts moving

        private float targetFillAmount;  // The target fill amount for both bars

        void Start()
        {
            targetFillAmount = 1f;  // Set initial fill amount (full health)
            instantFillBar.fillAmount = targetFillAmount;
            lazyFillBar.fillAmount = targetFillAmount;
        }

        // Call this function to update the health bar value
        public void Set(float healthPercentage)
        {
            // Set the instant bar to the target value immediately
            instantFillBar.fillAmount = healthPercentage;

            // Update the target fill amount for the lazy bar
            targetFillAmount = healthPercentage;

            // Stop any previous lazy bar movement
            StopAllCoroutines();
        
            // Start the lazy bar movement with a delay
            StartCoroutine(LazyBarRoutine());
        }

        private IEnumerator LazyBarRoutine()
        {
            // Wait for the specified delay
            yield return new WaitForSeconds(delay);

            // Move the lazy bar towards the target value
            while (Mathf.Abs(lazyFillBar.fillAmount - targetFillAmount) > 0.01f)
            {
                lazyFillBar.fillAmount = Mathf.Lerp(lazyFillBar.fillAmount, targetFillAmount, lazySpeed * Time.deltaTime);
                yield return null;
            }

            // Ensure it reaches exactly the target value
            lazyFillBar.fillAmount = targetFillAmount;
        }
    }

}