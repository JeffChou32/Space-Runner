using UnityEngine;
using UnityEngine.UI;

public class ObjectiveText : MonoBehaviour
{
    public Text objectiveText; // Reference to the Text object
    public float initialDisplayTime = 5f; // Time to show the initial message (in seconds)
    public float objectiveTime = 240f; // Duration of the objective (4 minutes)
    public float objectiveDistance = 7000;
    public float finalMessageDisplayTime = 5f; // Time to show the success/failure message

    private float elapsedTime = 0f; // Tracks elapsed time
    private bool objectiveStarted = false; // Tracks whether the objective has started
    private bool finalMessageShown = false; // Tracks if the final message has been shown
    private float finalMessageTimer = 0f; // Tracks time for the final message display
    public static bool objectiveFinished;
    public Text fastestTime;
    private int finishTime;

    void Start()
    {
        // Display the initial objective text
        ShowText($"Objective: Travel {objectiveDistance} km in 4 minutes");
        objectiveStarted = true;
        objectiveFinished = false;
        int storedFastestTime = PlayerPrefs.GetInt("FastestTime", int.MaxValue);
        if (storedFastestTime != int.MaxValue)
        {            
            fastestTime.text = $"{FormatTime(storedFastestTime)}";
        }
        else
        {
            fastestTime.text = "n/a";
        }
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
            if (LogicScript.playerScore >= objectiveDistance && !finalMessageShown)
            {
                ShowText($"{objectiveDistance} km traveled. Objective complete in {FormatTime((int)elapsedTime)}!");
                finishTime = (int)elapsedTime;
                finalMessageShown = true;
                objectiveFinished = true;
                StartFinalMessageTimer();
            }

            // Check for failure condition if time runs out
            if (elapsedTime >= objectiveTime && !finalMessageShown)
            {
                ShowText($"4 minutes have passed. \n{LogicScript.playerScore} km traveled. \nObjective failed");
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
        if (objectiveFinished)
        {            
            int storedFastestTime = PlayerPrefs.GetInt("FastestTime", int.MaxValue);
            if (finishTime < storedFastestTime)
            {
                fastestTime.text = FormatTime(finishTime);
                PlayerPrefs.SetInt("FastestTime", finishTime);  // Save new high score
                PlayerPrefs.Save();  // Ensure the score is stored persistently
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
    private string FormatTime(int time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("FastestTime");
        PlayerPrefs.Save();  // Ensures the data is cleared from disk
    }
}