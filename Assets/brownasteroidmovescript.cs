using System.Collections.Generic;
using UnityEngine;

public class brownasteroidmovescript : MonoBehaviour
{
    public float moveSpeed = 5;
    private float originalMoveSpeed;
    public float deadZone = -20;
    public float explodeRadius = 0;
    
    void Start()
    {
        originalMoveSpeed = moveSpeed;        
    }
    
    void Update()
    {
        moveSpeed = originalMoveSpeed * shipscript.multiplier;              
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


