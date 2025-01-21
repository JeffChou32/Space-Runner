using UnityEngine;

public class speedboosthud : MonoBehaviour
{
    public GameObject speedBoostContainer; // Container with Horizontal Layout Group
    public GameObject speedBoostIconPrefab; // Prefab for a single icon

    // Updates the number of icons based on the speed boost count
    public void UpdateSpeedBoostIcons(int speedBoostCount)
    {
        // Clear existing icons
        foreach (Transform child in speedBoostContainer.transform)
        {
            Destroy(child.gameObject);
        }

        // Add icons based on speedBoostCount
        for (int i = 0; i < speedBoostCount; i++)
        {
            Instantiate(speedBoostIconPrefab, speedBoostContainer.transform);
        }
    }
}
