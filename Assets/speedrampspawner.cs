using UnityEngine;

public class speedrampspawner : MonoBehaviour
{
    public GameObject speedRamp;
    public float minSpawnRate = 1f; // Minimum spawn interval
    public float maxSpawnRate = 3f; // Maximum spawn interval
    public float heightOffset = 10;

    private float currentSpawnRate; // Current spawn interval
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetRandomSpawnRate(); // Set an initial random spawn interval
        spawnSpeedBoost();
    }

    // Update is called once per frame
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
            SetRandomSpawnRate(); // Set a new random spawn interval
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