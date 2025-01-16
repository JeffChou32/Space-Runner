using UnityEngine;

public class asteroidSpawnerBrown : MonoBehaviour
{
    //public GameObject asteroid;
    public GameObject[] asteroidPrefabs;
    public float heightOffset = 10;
    public float spawnYThreshold = -10f;
    private GameObject lastSpawnedAsteroid;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnAsteroid();
    }

    void Update()
    {

        if (lastSpawnedAsteroid != null && lastSpawnedAsteroid.transform.position.y <= spawnYThreshold)
        {
            spawnAsteroid();
        }

    }
    void spawnAsteroid()
    {
        float lowestPoint = transform.position.x - heightOffset;
        float highestPoint = transform.position.x + heightOffset;

        int randomIndex = Random.Range(0, asteroidPrefabs.Length);
        GameObject randomAsteroid = asteroidPrefabs[randomIndex];
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        lastSpawnedAsteroid = Instantiate(randomAsteroid, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, 0), randomRotation);
    }

}
