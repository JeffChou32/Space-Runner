using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Transform ship;
    public float followSpeed = 5f; 
    public Vector3 offset;

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        if (ship != null)
        {
            Vector3 targetPosition = new Vector3(ship.position.x + offset.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);            
        }
    }
}
