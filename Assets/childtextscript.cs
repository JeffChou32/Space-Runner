using UnityEngine;
using UnityEngine.UI;

public class childtextscript : MonoBehaviour
{
    private Text childText;
    public Text parentText; // Assign the parent Text component in the Inspector or dynamically

    void Start()
    {
        // Get the Text component on this GameObject
        childText = GetComponent<Text>();

        // If parentText is not assigned, try to find it on the parent
        if (parentText == null && transform.parent != null)
        {
            parentText = transform.parent.GetComponent<Text>();
        }
    }

    void Update()
    {
        // Sync the color with the parent text if both are assigned
        if (childText != null && parentText != null)
        {
            childText.color = parentText.color;
        }
    }
}
