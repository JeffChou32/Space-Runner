using UnityEngine;

public class ResizeDistanceRect : MonoBehaviour
{
    public RectTransform uiRectangle; // The RectTransform of the rectangle
    public float maxScore = 10000f; // The maximum score
    public float maxHeight = 200f; // The maximum height of the rectangle

    void Update()
    {
        if (ObjectiveText.objectiveFinished) return;
        if (uiRectangle != null)
        {
            // Get the current score from Logicscript
            float currentScore = LogicScript.playerScore;

            // Clamp the score to ensure it doesn't exceed the maximum
            currentScore = Mathf.Clamp(currentScore, 0, maxScore);

            // Calculate the new height based on the score
            float newHeight = Mathf.Lerp(0, maxHeight, currentScore / maxScore);

            // Update the sizeDelta to adjust the rectangle's height
            uiRectangle.sizeDelta = new Vector2(uiRectangle.sizeDelta.x, newHeight);
        }
    }
}
