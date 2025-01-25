using UnityEngine;

public class speedboostscript : MonoBehaviour
{
    public float moveSpeed = 5;
    public float deadZone = -20;
    private static float originalMoveSpeed;
    
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
            Debug.Log("speedboost Deleted");
            Destroy(gameObject);
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        shipscript ship = collision.GetComponent<shipscript>();

        if (ship != null)
        {
            ship.speedBoosts += 1;
            speedhud.boosts += 1;            
            Debug.Log("Destroying speedboost object: " + gameObject.name);
            Destroy(gameObject);
        }
    }    
}
