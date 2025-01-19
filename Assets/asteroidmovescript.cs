using UnityEngine;

public class asteroidmovescript : MonoBehaviour
{    
    public float moveSpeed = 5;
    private static float originalMoveSpeed;
    public float deadZone = -20;
    public float explodeRadius = 0;
    private float targetMoveSpeed;
    public float speedChangeRate = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalMoveSpeed = moveSpeed;
        targetMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //if (shipscript.boost) explode();
        targetMoveSpeed = originalMoveSpeed * shipscript.multiplier;
        //moveSpeed = originalMoveSpeed * shipscript.multiplier;
        moveSpeed = Mathf.MoveTowards(moveSpeed, targetMoveSpeed, speedChangeRate * Time.deltaTime);
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        if (transform.position.y < deadZone)
        {
            Debug.Log("Asteroid Deleted");
            Destroy(gameObject);
        }   
    }
    private void explode()
    {
        shipscript ship = FindAnyObjectByType<shipscript>(); // Find the ship in the scene

        if (ship != null)
        {
            float distance = Vector3.Distance(transform.position, ship.transform.position);

            // Collect if within the radius, regardless of ship collider state
            if (distance <= explodeRadius)
            {
                Debug.Log("Asteroid Exploded");
                Destroy(gameObject);
            }
        }
    }
}
