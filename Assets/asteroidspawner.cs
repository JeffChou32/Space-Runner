using UnityEngine;

public class asteroidspawner : MonoBehaviour
{
    public GameObject asteroid;
    //public GameObject[] asteroidPrefabs;
    public float heightOffset = 10;    
    public float spawnYThreshold = -10f;
    private GameObject lastSpawnedAsteroid;
  

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
        lastSpawnedAsteroid = Instantiate(asteroid, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y, 0), transform.rotation);
    }   

}
