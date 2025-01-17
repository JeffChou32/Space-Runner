using UnityEngine;

public class swayscript : MonoBehaviour
{
    private ParticleSystem myParticleSystem;
    private Vector3 lastPosition;
    private Vector3 velocity;

    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        lastPosition = transform.position;
    }

    
    void Update()
    {
        velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;

        // Apply the opposite force to the particles
        var velocityOverLifetime = myParticleSystem.velocityOverLifetime;
        velocityOverLifetime.enabled = true;

        // Set velocity in the opposite direction of movement
        velocityOverLifetime.x = -velocity.x;
        velocityOverLifetime.y = -velocity.y;
        velocityOverLifetime.z = -velocity.z;
    }
}
