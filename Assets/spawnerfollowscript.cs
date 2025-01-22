using UnityEngine;

public class spawnerfollowscript : MonoBehaviour
{
    public Transform ship;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ship != null)
        {
            // Update the x position to match the ship's x position
            transform.position = new Vector3(ship.position.x, transform.position.y, transform.position.z);
        }
    }
}
