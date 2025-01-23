using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructionmenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
