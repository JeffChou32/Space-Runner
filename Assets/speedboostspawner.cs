using UnityEngine;

public class speedboostspawner : MonoBehaviour
{
    public GameObject speedBoost;
    public float spawnRate = 2;
    private float originalSpawnRate;
    private float timer = 0;
    public float heightOffset = 10;
  
    void Start()
    {
        originalSpawnRate = spawnRate;        
    }

    
    void Update()
    {
        spawnRate = originalSpawnRate / shipscript.multiplier;
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnSpeedBoost();
            timer = 0;
        }

    }
    void spawnSpeedBoost()
    {
        float lowestPoint = transform.position.x - heightOffset;
        float highestPoint = transform.position.x + heightOffset;
        Instantiate(speedBoost, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, 0), transform.rotation);
    }
}
