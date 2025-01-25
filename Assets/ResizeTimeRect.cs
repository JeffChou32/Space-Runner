using UnityEngine;

public class ResizeTimeRectangle : MonoBehaviour
{
    public RectTransform uiRectangle; // The RectTransform of the rectangle
    public float targetHeight = 200f; // The desired height
    public float duration = 300f; // Duration to reach the target height (in seconds)

    private float elapsedTime = 0f; // Tracks the elapsed time    

    void Update()
    {
        if (ObjectiveText.objectiveFinished)
        {
            return;
        }
        if (uiRectangle != null && elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the current height based on interpolation
            float newHeight = Mathf.Lerp(0, targetHeight, elapsedTime / duration);

            // Update the sizeDelta to adjust the rectangle's height
            uiRectangle.sizeDelta = new Vector2(uiRectangle.sizeDelta.x, newHeight);
        }
    }
}
