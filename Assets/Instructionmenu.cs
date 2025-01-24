using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Instructionmenu : MonoBehaviour
{
    private void Update()
    {
       
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
