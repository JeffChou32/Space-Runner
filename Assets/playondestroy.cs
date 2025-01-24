using UnityEngine;

public class playondestroy : MonoBehaviour
{
    public AudioClip destructionSound; // Sound to play when destroyed
    public float volume = 1.0f; // Volume of the sound

    void OnDestroy()
    {
        // Play the sound at the object's position in a 2D context
        AudioSource.PlayClipAtPoint(destructionSound, transform.position, volume);
    }
}
