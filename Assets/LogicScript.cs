using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LogicScript : MonoBehaviour
{
    public static int playerScore;
    public Text scoreText;
    public int speed;
    public float distance;
    public GameObject gameOverScreen;
    public bool isGameOver = false;
    public Text highScore;
    public Text mult;
    public Color sample;
    public RectTransform boostBar; 
    public float baseBarWidth = 100f;
    public Text currentSpeedtxt;
    public Text timerText;
    public float elapsedTime = 0f;
    public Text warning;    

    private void Start()
    {        
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0); 
        highScore.text = storedHighScore.ToString();
        playerScore = 0;
    }
    void Update()
    {
        // Accumulate distance based on speed and time
        var boostBarImage = boostBar.GetComponent<Image>();
        if (!isGameOver)
        {
            elapsedTime += Time.deltaTime;
            distance += speed * Time.deltaTime;
            warning.gameObject.SetActive(shipscript.waitingForReturn);

            // If 1 second has passed, increase score by speed
            while (distance >= 1f)
            {
                playerScore += (1*shipscript.multiplier);
                scoreText.text = playerScore.ToString();
                distance -= 1f;  // Reset timer
            }
            float currentSpeed = speed * shipscript.multiplier; 
            currentSpeedtxt.text = $"{currentSpeed:F0}";
            if (shipscript.multiplier > 1)
            {
                mult.text = $"x{shipscript.multiplier}";
                mult.gameObject.SetActive(true);
                if (shipscript.multiplier == 2)
                {
                    Color color = new Color(216f / 255f, 121f / 255f, 26f / 255f, 1);
                    mult.color = color;
                    boostBarImage.color = color;                    
                }
                if (shipscript.multiplier == 3)
                {
                    Color color = new Color(79f / 255f, 153f / 255f, 207f / 255f, 1);
                    mult.color = color;
                    boostBarImage.color = color;                    
                }
                if (shipscript.multiplier > 3)
                {
                    Color color = new Color(202f / 255f, 230f / 255f, 255f / 255f, 1);
                    mult.color = color; 
                    boostBarImage.color = color;                    
                }
                mult.fontSize = Mathf.Min(100 + (shipscript.multiplier - 1) * 10, 200);
            }
            else
            {
                mult.text = ""; // Clear the text
                mult.gameObject.SetActive(false);
            }
        }
        boostBar.sizeDelta = new Vector2(shipscript.boostTimer * 50, boostBar.sizeDelta.y);        
        timerText.text = FormatTime(elapsedTime);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        mult.text = ""; // Clear the text
        mult.gameObject.SetActive(false);
        boostBar.gameObject.SetActive(false);
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
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60); 
        int seconds = Mathf.FloorToInt(time % 60); 
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }    
}
