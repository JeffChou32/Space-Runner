using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public int speed;
    public float distance; // Tracks fractional distance for updates
    public GameObject gameOverScreen;
    public bool isGameOver = false;
    public Text highScore;

    private void Start()
    {
        // Load the stored high score at the start of the game
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0); // Default is 0 if no score is saved
        highScore.text = storedHighScore.ToString();
    }
    void Update()
    {
        // Accumulate distance based on speed and time
        if (!isGameOver)
        {
            distance += speed * Time.deltaTime;

            // If 1 second has passed, increase score by speed
            while (distance >= 1f)
            {
                playerScore += (1*shipscript.multiplier);
                scoreText.text = playerScore.ToString();
                distance -= 1f;  // Reset timer
            }
        }
               
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        isGameOver = true;        
        gameOverScreen.SetActive(true);     
            
        // Check and update high score using PlayerPrefs
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if (playerScore > storedHighScore)
        {
            highScore.text = playerScore.ToString();
            PlayerPrefs.SetInt("HighScore", playerScore);  // Save new high score
            PlayerPrefs.Save();  // Ensure the score is stored persistently
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.Save();  // Ensures the data is cleared from disk
    }
}
