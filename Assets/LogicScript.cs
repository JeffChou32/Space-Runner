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
    public Text mult;
    public Color sample;
    public RectTransform boostBar; 
    public float baseBarWidth = 100f;
    public Text currentSpeedtxt;
    private Color defaultColor;

    private void Start()
    {
        // Load the stored high score at the start of the game
        int storedHighScore = PlayerPrefs.GetInt("HighScore", 0); // Default is 0 if no score is saved
        highScore.text = storedHighScore.ToString();
        defaultColor = currentSpeedtxt.color;
    }
    void Update()
    {
        // Accumulate distance based on speed and time
        var boostBarImage = boostBar.GetComponent<Image>();
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
                    currentSpeedtxt.color = color;
                }
                if (shipscript.multiplier == 3)
                {
                    Color color = new Color(79f / 255f, 153f / 255f, 207f / 255f, 1);
                    mult.color = color;
                    boostBarImage.color = color;
                    currentSpeedtxt.color = color;
                }
                if (shipscript.multiplier > 3)
                {
                    Color color = new Color(202f / 255f, 230f / 255f, 255f / 255f, 1);
                    mult.color = color; 
                    boostBarImage.color = color;
                    currentSpeedtxt.color = color;
                }

                mult.fontSize = Mathf.Min(80 + (shipscript.multiplier - 1) * 10, 160);
            }
            else
            {
                mult.text = ""; // Clear the text
                mult.gameObject.SetActive(false);
                currentSpeedtxt.color = defaultColor;
            }
        }
        boostBar.sizeDelta = new Vector2(shipscript.boostTimer * 50, boostBar.sizeDelta.y);      
  
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
