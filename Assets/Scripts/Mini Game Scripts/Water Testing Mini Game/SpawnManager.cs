using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] trashOnGroundPrefabs;
    public GameObject[] trashInRiverPrefabs;
    public GameObject[] fishPrefabs;

    private int numberOfTrashOnGroundToSpawn = 15;
    private int maximumTrashInRiver = 20;
    private int spawnedTrashInRiverCount = 0;
    private int minimumFishInRiver = 8;
    private int maximumFishInRiver = 20;
    private int spawnedFishInRiverCount = 0;

    private float spawnTime = 0.05f;
    private float spawnDelay = 2.5f;
    private float minimumXOnGround = -285f;
    private float maximumXOnGround = 240f;
    private float yPositionOnGround = 0f;
    private float minimumZOnGround = -20f;
    private float maximumZOnGround = 226f;
    private float xPositionInRiver = -282f;
    private float yPositionInRiver = -20f;
    private float minimumZInRiver = -126.5f;
    private float maximumZInRiver = -61.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnTrashOnGround();
        InvokeRepeating("SpawnTrashInRiver", spawnTime, spawnDelay); // repeatedly invoke SpawnTrashInRiver()
        InvokeRepeating("SpawnFish", spawnTime, spawnDelay); // repeatedly invoke SpawnFish()
    }

    // Update is called once per frame
    void Update()
    {
        OnTrashDestroyed();
        OnFishDestroyed();
    }
    // Spawn trash on the ground
    private void SpawnTrashOnGround()
    {
        // For, i equals 0, i is less than numberOfTrashOnGroundToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfTrashOnGroundToSpawn; i++)
        {
            // If bool isTrashCollected is false...
            if (!TestTransitionsGameManager.isTrashCollected)
            {
                int trashOnGroundIndex = Random.Range(0, trashOnGroundPrefabs.Length); // trashOnGroundIndex equals a number with in range of 0 to 2
                Instantiate(trashOnGroundPrefabs[trashOnGroundIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                    yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), trashOnGroundPrefabs[trashOnGroundIndex].transform.rotation); // Instantiate trashOnGroundPrefab at trashOnGroundIndex at new Vector3
            }
        }
    }

    // Spawn trash in the river
    private void SpawnTrashInRiver()
    {
        // If bool isTrashCollected is false...
        if (!TestTransitionsGameManager.isTrashCollected)
        {
            // If spawnedTrashInRiverCount is less than maximumTrashInRiver...
            if (spawnedTrashInRiverCount < maximumTrashInRiver)
            {
                int trashInRiverIndex = Random.Range(0, trashInRiverPrefabs.Length); // trashInRiverIndex equals a number with in range of 0 to 1
                Instantiate(trashInRiverPrefabs[trashInRiverIndex], new Vector3(xPositionInRiver, yPositionInRiver,
                    Random.Range(minimumZInRiver, maximumZInRiver)), trashInRiverPrefabs[trashInRiverIndex].transform.rotation); // Instantiate trashInRiverPrefab at trashInRiverIndex at new Vector3
                trashInRiverPrefabs[trashInRiverIndex].gameObject.tag = "Destructible"; // Set tag for all trashInRiverPrefabs within trshInRiverIndex to "Destructible"
                spawnedTrashInRiverCount++; // spawnedTrashInRiverCount equals itself plus 1
            }
        }
    }

    // Spawn fish in the river
    private void SpawnFish()
    {
        // If bool isTrashCollected is false...
        if (!TestTransitionsGameManager.isTrashCollected)
        {
            // If spawnedFishInRiverCount is less than minimumFishInRiver...
            if (spawnedFishInRiverCount < minimumFishInRiver)
            {
                int fishIndex = Random.Range(0, fishPrefabs.Length); // fishIndex equals a number with in range of 0 to 3
                Instantiate(fishPrefabs[fishIndex], new Vector3(xPositionInRiver, yPositionInRiver, Random.Range(minimumZInRiver, maximumZInRiver)),
                    fishPrefabs[fishIndex].transform.rotation); // Instantiate fishPrefab at fishIndex at new Vector3
                spawnedFishInRiverCount++; // spawnedFishInRiverCount equals itself plus 1
            }
        }

        // If bool isTrashCollected is true...
        if (TestTransitionsGameManager.isTrashCollected)
        {
            // If spawnedFishInRiverCount is less than maximumFishInRiver...
            if (spawnedFishInRiverCount < maximumFishInRiver)
            {
                int fishIndex = Random.Range(0, fishPrefabs.Length); // fishIndex equals a number with in range of 0 to 3
                Instantiate(fishPrefabs[fishIndex], new Vector3(xPositionInRiver, yPositionInRiver, Random.Range(minimumZInRiver, maximumZInRiver)),
                    fishPrefabs[fishIndex].transform.rotation); // Instantiate fishPrefab at fishIndex at new Vector3
                spawnedFishInRiverCount++; // spawnedFishInRiverCount equals itself plus 1
            }
        }
    }

    // Call method when trash is destroyed
    public void OnTrashDestroyed()
    {
        // If spawnedTrashInRiverCount is greater than zero...
        if (spawnedTrashInRiverCount > 0)
        {
            spawnedTrashInRiverCount--; // spawnedTrashInRiverCount is equal to itself minus 1
        }
    }

    // Call method with fish are destroyed
    public void OnFishDestroyed()
    {
        // If spawnedFishInRiverCount is greater than zero...
        if (spawnedFishInRiverCount > 0)
        {
            spawnedFishInRiverCount--; // spawnedFishInRiverCount is equal to itself minus 1
        }
    }
}
