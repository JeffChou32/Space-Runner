using UnityEngine;

public class spawnerManager : MonoBehaviour
{
    public GameObject asteroidSpawnerPrefab; // Reference to the spawner prefab    
    public Transform ship; // Reference to the ship
    public float spawnThreshold = 15f; // Distance from the ship to trigger new spawners
    public float spawnerSpacing = 30f; // Distance between consecutive spawners

    private float lastSpawnedXLeft;
    private float lastSpawnedXRight;
    public float despawnDistance = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnedXLeft = ship.position.x - spawnerSpacing;
        lastSpawnedXRight = ship.position.x + spawnerSpacing;
        Instantiate(asteroidSpawnerPrefab, new Vector3(lastSpawnedXLeft, transform.position.y, 0), Quaternion.identity);
        Instantiate(asteroidSpawnerPrefab, new Vector3(lastSpawnedXRight, transform.position.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.position.x < lastSpawnedXLeft - spawnThreshold)
        {
            lastSpawnedXLeft -= spawnerSpacing;
            Instantiate(asteroidSpawnerPrefab, new Vector3(lastSpawnedXLeft, transform.position.y, 0), Quaternion.identity);
        }

        if (ship.position.x > lastSpawnedXRight + spawnThreshold)
        {
            lastSpawnedXRight += spawnerSpacing;
            Instantiate(asteroidSpawnerPrefab, new Vector3(lastSpawnedXRight, transform.position.y, 0), Quaternion.identity);
        }
        DestroySpawnersOutOfRange();
    }
    void DestroySpawnersOutOfRange()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("AsteroidSpawner");

        foreach (GameObject spawner in spawners)
        {
            float distanceToShip = Mathf.Abs(spawner.transform.position.x - ship.position.x);

            if (distanceToShip > despawnDistance)
            {
                Destroy(spawner);
            }
        }
    }
}
