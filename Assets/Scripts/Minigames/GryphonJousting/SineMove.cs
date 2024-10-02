using UnityEngine;

public class SineMove : MonoBehaviour
{
    RectTransform rectTransform;
    public Vector3 startPosition;  // The starting position
    public Vector3 endPosition;    // The position the transform moves towards during inhalation
    public float speed = 1f;       // Speed of the breathing cycle (affects the frequency of the sine wave)

    private float timeCounter = 0f;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Increment time counter based on the speed of the cycle
        timeCounter += Time.deltaTime * speed;

        // Calculate a value between 0 and 1 based on the sine wave
        float t = (Mathf.Sin(timeCounter) + 1f) / 2f;

        // Move the transform between the start and end positions
        rectTransform.anchoredPosition = Vector3.Lerp(startPosition, endPosition, t);
    }
}