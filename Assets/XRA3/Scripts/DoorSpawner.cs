using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{

    public GameObject doorPrefab;
    public float doorSpeed = 8f;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 6f;

    public float spawnRadius = 0.9f;

    public GameObject referencePoint;

    public Vector3 minScale;
    public Vector3 maxScale;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnFrameRandomly", Random.Range(minSpawnTime, maxSpawnTime));
    }


    public void SpawnFrameRandomly(){
        // Generate a random point inside a circle of spawnRadius
        Vector2 randomPointInCircle = Random.insideUnitCircle * spawnRadius;

        // Create a new position using the reference point and the random point
        Vector3 spawnPosition = new Vector3(
            referencePoint.transform.position.x + randomPointInCircle.x,
            referencePoint.transform.position.y,
            referencePoint.transform.position.z + randomPointInCircle.y
        );

        // Instantiate the object prefab at the spawn position
        GameObject obj = Instantiate(doorPrefab, spawnPosition, Quaternion.identity);
        obj.transform.localScale = new Vector3(Random.Range(minScale.x, maxScale.x), Random.Range(minScale.y, maxScale.y), Random.Range(minScale.z, maxScale.z));

         bool isGameOver = LevelManager.Instance.isGameOver();
        if (!isGameOver){
            Invoke("SpawnFrameRandomly", Random.Range(minSpawnTime, maxSpawnTime));    
        }
        
    }

}
