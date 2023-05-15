using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectPrefabs;

    public GameObject doorPrefab;
    public float spawnRadius = 1.2f;

    public GameObject referencePoint;


    public void SpawnGameObjectsRandomly(){
        //Randomly choose a prefab
        GameObject gameObjectToInstantiate = objectPrefabs[Random.Range(0, objectPrefabs.GetLength(0))];

        // Generate a random point inside a circle of spawnRadius
        Vector2 randomPointInCircle = Random.insideUnitCircle * spawnRadius;

        // Create a new position using the reference point and the random point
        Vector3 spawnPosition = new Vector3(
            referencePoint.transform.position.x + randomPointInCircle.x,
            referencePoint.transform.position.y,
            referencePoint.transform.position.z + randomPointInCircle.y
        );

        // Instantiate the object prefab at the spawn position
        GameObject obj = Instantiate(gameObjectToInstantiate, spawnPosition, Quaternion.identity);
        obj.AddComponent<SphereCollider>();
    }

    
}
