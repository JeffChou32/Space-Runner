using System.Collections.Generic;
using UnityEngine;

public class brownasteroidmovescript : MonoBehaviour
{
    public float moveSpeed = 5;
    private float originalMoveSpeed;
    public float deadZone = -20;
    public float explodeRadius = 0;
    public GameObject explosionEffect;

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
        explode();
    }
    private void explode()
    {
        shipscript ship = FindAnyObjectByType<shipscript>(); // Find the ship in the scene

        if (ship != null)
        {
            float distance = Vector3.Distance(transform.position, ship.transform.position);
            int shipLayer = ship.gameObject.layer;
            int asteroidLayer = gameObject.layer;

            if (Physics2D.GetIgnoreLayerCollision(shipLayer, asteroidLayer) && distance <= explodeRadius)
            {
                Debug.Log("Asteroid Exploded");
                // Instantiate the explosion effect at the asteroid's position
                Instantiate(explosionEffect, transform.position, Quaternion.identity);

                // Destroy the asteroid
                Destroy(gameObject);
            }
        }
    }
}


