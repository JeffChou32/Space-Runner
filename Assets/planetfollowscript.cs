using UnityEngine;

public class planetfollowscript : MonoBehaviour
{
    private Transform target; // The target to follow (e.g., the main camera)
    public float followSpeed = 5f; // Speed at which the prefab follows the target
    public float speedMultiplier = 0.8f; // Multiplier to make the prefab move slower than the camera

    private Vector3 lastTargetPosition;

    void Start()
    {
        if (target == null)
        {
            // Automatically find the main camera if no target is assigned
            target = Camera.main.transform;
        }

        // Initialize the last known position of the target
        lastTargetPosition = target.position;
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the camera's velocity (movement per frame)
            Vector3 targetVelocity = (target.position - lastTargetPosition) / Time.deltaTime;

            // Calculate the speed for the prefab (slightly slower than the camera)
            Vector3 desiredMovement = targetVelocity * speedMultiplier * Time.deltaTime;

            // Move the prefab towards the target's current position
            transform.position += desiredMovement;

            // Update the last known position of the target
            lastTargetPosition = target.position;
        }
    }
}