using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashingText : MonoBehaviour
{
    public Text textComponent; // Reference to the Text component
    public float flashSpeed = 0.5f; // Speed of flashing (in seconds)
    private bool isFlashing = false;

    void Start()
    {
        // Automatically get the Text component if not assigned
        if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }

        // Start flashing
        StartFlashing();
    }

    public void StartFlashing()
    {
        if (!isFlashing)
        {
            isFlashing = true;
            StartCoroutine(Flash());
        }
    }

    public void StopFlashing()
    {
        isFlashing = false;
        StopAllCoroutines();
        textComponent.enabled = true; // Ensure the text is visible when stopped
    }

    private IEnumerator Flash()
    {
        while (isFlashing)
        {
            textComponent.enabled = !textComponent.enabled; // Toggle visibility
            yield return new WaitForSeconds(flashSpeed);
        }
    }
}