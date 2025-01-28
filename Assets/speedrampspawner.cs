using UnityEngine;

public class speedrampspawner : MonoBehaviour
{
    public GameObject speedRamp;
    public float minSpawnRate = 1f; 
    public float maxSpawnRate = 3f; 
    public float heightOffset = 10;

    private float currentSpawnRate; 
    private float timer = 0;
    private float originalMinSpawnRate;
    private float originalMaxSpawnRate;

    void Start()
    {
        SetRandomSpawnRate();
        originalMinSpawnRate = minSpawnRate;
        originalMaxSpawnRate = maxSpawnRate;

    }
        
    void Update()
    {
        if (timer < currentSpawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnSpeedBoost();
            timer = 0;
            SetRandomSpawnRate(); 
        }
        if (shipscript.multiplier > 5)
        {
            minSpawnRate = 2;
            maxSpawnRate = 4;
        } else
        {
            minSpawnRate = originalMinSpawnRate;
            maxSpawnRate = originalMaxSpawnRate;
        }

    }
    void spawnSpeedBoost()
    {
        // Spawn a speed boost at a random position within the height offset
        float lowestPoint = transform.position.x - heightOffset;
        float highestPoint = transform.position.x + heightOffset;
        Instantiate(speedRamp, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, 0), transform.rotation);
    }

    void SetRandomSpawnRate()
    {
        // Calculate a random spawn interval between minSpawnRate and maxSpawnRate
        currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }
}