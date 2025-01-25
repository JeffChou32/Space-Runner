using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    public Text objectiveText; // Reference to the Text object
    public float initialDisplayTime = 5f; // Time to show the initial message (in seconds)
    public float objectiveTime = 240f; // Duration of the objective (4 minutes)
    public float finalMessageDisplayTime = 5f; // Time to show the success/failure message

    private float elapsedTime = 0f; // Tracks elapsed time
    private bool objectiveStarted = false; // Tracks whether the objective has started
    private bool finalMessageShown = false; // Tracks if the final message has been shown
    private float finalMessageTimer = 0f; // Tracks time for the final message display

    void Start()
    {
        // Display the initial objective text
        ShowText("Objective: Travel 7,000 km in 4 minutes");
        objectiveStarted = true;
    }

    void Update()
    {
        if (objectiveStarted)
        {
            elapsedTime += Time.deltaTime;

            // Hide the text after the initial display time
            if (elapsedTime > initialDisplayTime && !finalMessageShown)
            {
                HideText();
            }

            // Check for success condition
            if (LogicScript.playerScore >= 10000 && !finalMessageShown)
            {
                ShowText("7,000 km traveled. Objective complete!");
                finalMessageShown = true;
                StartFinalMessageTimer();
            }

            // Check for failure condition if time runs out
            if (elapsedTime >= objectiveTime && !finalMessageShown)
            {
                ShowText("4 minutes have passed. Objective failed");
                finalMessageShown = true;
                StartFinalMessageTimer();
            }

            // Hide the final message after the display time
            if (finalMessageShown)
            {
                finalMessageTimer += Time.deltaTime;
                if (finalMessageTimer >= finalMessageDisplayTime)
                {
                    HideText();
                    objectiveStarted = false; // Stop further updates
                }
            }
        }
    }

    // Show the text with a specific message
    void ShowText(string message)
    {
        objectiveText.text = message;
        objectiveText.enabled = true;
    }

    // Hide the text
    void HideText()
    {
        objectiveText.enabled = false;
    }

    // Start the timer for the final message
    void StartFinalMessageTimer()
    {
        finalMessageTimer = 0f; // Reset the timer
    }
}