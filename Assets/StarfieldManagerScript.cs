using UnityEngine;

public class StarfieldManagerScript : MonoBehaviour
{
    public GameObject starfieldSpawnerPrefab; // Reference to the spawner prefab    
    public Transform ship; // Reference to the ship
    public float spawnThreshold = 15f; // Distance from the ship to trigger new spawners
    public float spawnerSpacing = 30f; // Distance between consecutive spawners

    private float lastSpawnedXLeft;
    private float lastSpawnedXRight;
    public float despawnDistance = 50f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastSpawnedXLeft = Mathf.Floor(ship.position.x / spawnerSpacing) * spawnerSpacing - spawnerSpacing;
        lastSpawnedXRight = Mathf.Floor(ship.position.x / spawnerSpacing) * spawnerSpacing + spawnerSpacing;
        Instantiate(starfieldSpawnerPrefab, new Vector3(lastSpawnedXLeft, transform.position.y, 0), Quaternion.identity);
        Instantiate(starfieldSpawnerPrefab, new Vector3(lastSpawnedXRight, transform.position.y, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ship.position.x < lastSpawnedXLeft - spawnThreshold)
        {
            lastSpawnedXLeft -= spawnerSpacing;
            Instantiate(starfieldSpawnerPrefab, new Vector3(lastSpawnedXLeft, transform.position.y, 0), Quaternion.identity);
        }

        if (ship.position.x > lastSpawnedXRight + spawnThreshold)
        {
            lastSpawnedXRight += spawnerSpacing;
            Instantiate(starfieldSpawnerPrefab, new Vector3(lastSpawnedXRight, transform.position.y, 0), Quaternion.identity);
        }
        DestroySpawnersOutOfRange();
    }
    void DestroySpawnersOutOfRange()
    {
        GameObject[] spawners = GameObject.FindGameObjectsWithTag("StarfieldSpawner");

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