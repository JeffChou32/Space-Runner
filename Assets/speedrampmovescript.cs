using UnityEngine;

public class speedrampmovescript : MonoBehaviour
{
    public float minMoveSpeed = 3f; // Minimum move speed
    public float maxMoveSpeed = 7f; // Maximum move speed
    private float moveSpeed;
    public float deadZone = -20;
    private static float originalMoveSpeed;
    private bool isInsideCollider = false; // Tracks whether the ship is inside the collider
    private shipscript ship;

    void Start()
    {
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        originalMoveSpeed = moveSpeed;
    }
       
    void Update()
    {
        float currentSpeed = originalMoveSpeed * shipscript.multiplier;
        transform.position = transform.position + (Vector3.down * currentSpeed) * Time.deltaTime;        
        if (transform.position.y < deadZone)
        {
            Debug.Log("speedboost Deleted");
            Destroy(gameObject);
        }
        if (isInsideCollider && ship != null && Input.GetKeyDown(KeyCode.Space) && !shipscript.waitingForReturn)
        {
            shipscript.ActivateSpeedRamp();           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shipscript potentialShip = collision.GetComponent<shipscript>();

        if (potentialShip != null)
        {
            ship = potentialShip; // Store the reference to the ship
            isInsideCollider = true; // Set the flag to true
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset the state when the ship exits the collider
        if (collision.GetComponent<shipscript>() == ship)
        {
            isInsideCollider = false;
            ship = null;
        }
    }
}
