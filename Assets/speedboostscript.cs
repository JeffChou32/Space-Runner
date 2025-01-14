using UnityEngine;

public class speedboostscript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -20;
    private static float originalMoveSpeed;
    //public float collectionRadius = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = originalMoveSpeed * shipscript.multiplier;
        transform.position = transform.position + (Vector3.down * moveSpeed) * Time.deltaTime;
        //CollectBoostIfClose();
        if (transform.position.y < deadZone)
        {
            Debug.Log("speedboost Deleted");
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the ShipScript component
        shipscript ship = collision.GetComponent<shipscript>();

        if (ship != null)
        {
            ship.speedBoosts += 1;
            ship.numBoosts.text = ship.speedBoosts.ToString();
            Destroy(gameObject); // Destroy the speed boost object after collection
        }
    }
    //private void CollectBoostIfClose()
    //{
    //    shipscript ship = FindAnyObjectByType<shipscript>(); // Find the ship in the scene

    //    if (ship != null)
    //    {
    //        float distance = Vector3.Distance(transform.position, ship.transform.position);

    //        // Collect if within the radius, regardless of ship collider state
    //        if (distance <= collectionRadius)
    //        {
    //            ship.speedBoosts += 1;
    //            ship.numBoosts.text = ship.speedBoosts.ToString();
    //            Debug.Log("Speed Boost Collected!");
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
