using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] aluminumCanPrefabs;
    public GameObject[] trashInRiverPrefabs;
    public GameObject[] fishPrefabs;
    public GameObject[] deerPrefabs;
    public GameObject beaverPrefab;
    public GameObject raccoonPrefab;
    //public GameObject testTubePrefab;
    public GameObject trashBagPrefab;
    public GameObject gasCanPrefab;
    public GameObject tirePrefab;

    private int numberOfDeerToSpawn = 4;
    private int numberOfBeaverToSpawn = 2;
    private int numberOfRaccoonToSpawn = 2;
    private int numberOfTrashBagsToSpawn = 15;
    private int numberOfGasCansToSpawn = 5;
    private int numberOfAluminumCansToSpawn = 5;
    private int numberOfTiresToSpawn = 5;
    private int maximumTrashInRiver = 30;
    private int spawnedTrashInRiverCount = 0;
    private int minimumFishInRiver = 8;
    private int maximumFishInRiver = 20;
    private int spawnedFishInRiverCount = 0;
    //private int maximumTestTubesInRiver = 20;
    private int spawnedTestTubeCount = 0;

    private float spawnTime = 0.05f;
    private float spawnDelay = 2.5f;
    private float minimumXOnGround = -50f;
    private float maximumXOnGround = -30f;
    private float yPositionOnGroundForDeer = 5f;
    private float yPositionOnGroundForBeaver = 1.4f;
    private float yPositionOnGroundForRaccoon = 2f;
    //private float yPositionOnGroundForTrashBag = 1.25f;
    //private float yPositionOnGroundForGasCan = 1.4f;
    //private float yPositionOnGroundForAluminumCan = 1.5f;
    //private float yPositionOnGroundForTire = 1f;
    private float minimumZOnGround = -152f;
    private float maximumZOnGround = -9f;
    private float minimumXInRiver = -23.5f;
    private float maximumXInRiver = 100f;
    private float yPositionInRiver = -1.75f;
    private float minimumZInRiver = -175.5f;
    private float maximumZInRiver = -175.5f;
    private float spawnRadius = 60f; // Radius for spawning trash on the ground

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!WaterTestingManager.isFirstWaterTestComplete)
        {
            SpawnMammals();
            SpawnTrashOnGround();
            InvokeRepeating("SpawnTrashInRiver", spawnTime, spawnDelay); // repeatedly invoke SpawnTrashInRiver()
            //InvokeRepeating("SpawnFish", spawnTime, spawnDelay); // repeatedly invoke SpawnFish()
            //InvokeRepeating("SpawnTestTube", spawnTime, spawnDelay); // repeatedly invoke SpawnTestTube()
        }

        if (WaterTestingManager.isFirstWaterTestComplete)
        {
            spawnDelay = 1f;
            numberOfDeerToSpawn = 6;
            numberOfBeaverToSpawn = 5;
            numberOfRaccoonToSpawn = 5;
            SpawnMammals();
            //InvokeRepeating("SpawnFish", spawnTime, spawnDelay); // repeatedly invoke SpawnFish()
            //InvokeRepeating("SpawnTestTube", spawnTime, spawnDelay); // repeatedly invoke SpawnTestTube()
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnTrashDestroyed();
        //OnFishDestroyed();
        //OnTestTubeDestroyed();
    }

    // Spawn Mammals
    private void SpawnMammals()
    {
        // For, i equals 0, i is less than numberOfMammalsToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfDeerToSpawn; i++)
        {
            int deerIndex = Random.Range(0, deerPrefabs.Length); // mammalIndex equals a number with in range of 0 to 2
            Instantiate(deerPrefabs[deerIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGroundForDeer, Random.Range(minimumZOnGround, maximumZOnGround)), deerPrefabs[deerIndex].transform.rotation); // Instantiate deerPrefab at deerIndex at new Vector3
        }

        // For, i equals 0, i is less than numberOfBeaverToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfBeaverToSpawn; i++)
        {
            Instantiate(beaverPrefab, new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGroundForBeaver, Random.Range(minimumZOnGround, maximumZOnGround)), beaverPrefab.transform.rotation); // Instantiate beaverPrefab at new Vector3
        }

        // For, i equals 0, i is less than numberOfRaccoonToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfRaccoonToSpawn; i++)
        {
            Instantiate(raccoonPrefab, new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGroundForRaccoon, Random.Range(minimumZOnGround, maximumZOnGround)), raccoonPrefab.transform.rotation); // Instantiate raccoonPrefab at new Vector3
        }
    }

    // Spawn trash on the ground
    private void SpawnTrashOnGround()
    {
        Vector3 centerPoint = new Vector3(-50f, 0f, -80f); // Center point for spawning trash

        // For, i equals 0, i is less than numberOfTrashBagsToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfTrashBagsToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition(centerPoint, spawnRadius);
            if (randomPosition != Vector3.zero)
            {
                Instantiate(trashBagPrefab, randomPosition, Quaternion.identity);
                Debug.Log($"Spawned trash at: {randomPosition}");
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position for trash.");
            }
            //Instantiate(trashBagPrefab, new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
            //    yPositionOnGroundForTrashBag, Random.Range(minimumZOnGround, maximumZOnGround)), trashBagPrefab.transform.rotation); // Instantiate trashBagPrefab at new Vector3
        }

        // For, i equals 0, i is less than numberOfGasCansToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfGasCansToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition(centerPoint, spawnRadius);
            if (randomPosition != Vector3.zero)
            {
                Instantiate(gasCanPrefab, randomPosition, Quaternion.identity);
                Debug.Log($"Spawned trash at: {randomPosition}");
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position for trash.");
            }
            //Instantiate(gasCanPrefab, new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
            //    yPositionOnGroundForGasCan, Random.Range(minimumZOnGround, maximumZOnGround)), gasCanPrefab.transform.rotation); // Instantiate gasCanPrefab at new Vector3
        }

        // For, i equals 0, i is less than numberOfAluminumCansToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfAluminumCansToSpawn; i++)
        {
            int aluminumCanIndex = Random.Range(0, aluminumCanPrefabs.Length); // aluminumCanIndex equals a number with in range of 0 to 2

            Vector3 randomPosition = GetRandomNavMeshPosition(centerPoint, spawnRadius);
            if (randomPosition != Vector3.zero)
            {
                Instantiate(aluminumCanPrefabs[aluminumCanIndex], randomPosition, Quaternion.identity);
                Debug.Log($"Spawned trash at: {randomPosition}");
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position for trash.");
            }
            //Instantiate(aluminumCanPrefabs[aluminumCanIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
            //    yPositionOnGroundForAluminumCan, Random.Range(minimumZOnGround, maximumZOnGround)), aluminumCanPrefabs[aluminumCanIndex].transform.rotation); // Instantiate aluminumCanPrefab at new Vector3
        }

        // For, i equals 0, i is less than numberOfTiresToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfTiresToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition(centerPoint, spawnRadius);
            if (randomPosition != Vector3.zero)
            {
                Instantiate(tirePrefab, randomPosition, Quaternion.identity);
                Debug.Log($"Spawned trash at: {randomPosition}");
            }
            else
            {
                Debug.LogWarning("Failed to find a valid NavMesh position for trash.");
            }
            //Instantiate(tirePrefab, new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
            //    yPositionOnGroundForTire, Random.Range(minimumZOnGround, maximumZOnGround)), tirePrefab.transform.rotation); // Instantiate tirePrefab at new Vector3
        }
    }

    // Spawn trash in the river
    private void SpawnTrashInRiver()
    {
        // If spawnedTrashInRiverCount is less than maximumTrashInRiver...
        if (spawnedTrashInRiverCount < maximumTrashInRiver)
        {
            int trashInRiverIndex = Random.Range(0, trashInRiverPrefabs.Length); // trashInRiverIndex equals a number with in range of 0 to 1
            Instantiate(trashInRiverPrefabs[trashInRiverIndex], new Vector3(Random.Range(minimumXInRiver, maximumXInRiver), yPositionInRiver,
                Random.Range(minimumZInRiver, maximumZInRiver)), trashInRiverPrefabs[trashInRiverIndex].transform.rotation); // Instantiate trashInRiverPrefab at new Vector3
            spawnedTrashInRiverCount++; // spawnedTrashInRiverCount equals itself plus 1
        }
    }

    //// Spawn test tube
    //private void SpawnTestTube()
    //{
    //    if (spawnedTestTubeCount < maximumTestTubesInRiver) // If spawnedTestTubeCount is less than maximumTestTubesInRiver...
    //    {
    //        Instantiate(testTubePrefab, new Vector3(Random.Range(minimumXInRiver, maximumXInRiver), yPositionInRiver,
    //            Random.Range(minimumZInRiver, maximumZInRiver)), testTubePrefab.transform.rotation); // Instantiate testTubePrefab at new Vector3
    //        spawnedTestTubeCount++; // spawnedTestTubeCount equals itself plus 1
    //    }
    //}

    // Spawn fish in the river
    private void SpawnFish()
    {
        // If bool isFirstWaterTestComplete is false...
        if (!WaterTestingManager.isFirstWaterTestComplete)
        {
            // If spawnedFishInRiverCount is less than minimumFishInRiver...
            if (spawnedFishInRiverCount < minimumFishInRiver)
            {
                int fishIndex = Random.Range(0, fishPrefabs.Length); // fishIndex equals a number with in range of 0 to 3
                Instantiate(fishPrefabs[fishIndex], new Vector3(Random.Range(minimumXInRiver, maximumXInRiver), yPositionInRiver, Random.Range(minimumZInRiver, maximumZInRiver)),
                    fishPrefabs[fishIndex].transform.rotation); // Instantiate fishPrefab at fishIndex at new Vector3
                spawnedFishInRiverCount++; // spawnedFishInRiverCount equals itself plus 1
            }
        }

        // If bool isFirstWaterTestComplete is true...
        if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            // If spawnedFishInRiverCount is less than maximumFishInRiver...
            if (spawnedFishInRiverCount < maximumFishInRiver)
            {
                int fishIndex = Random.Range(0, fishPrefabs.Length); // fishIndex equals a number with in range of 0 to 3
                Instantiate(fishPrefabs[fishIndex], new Vector3(Random.Range(minimumXInRiver, maximumXInRiver), yPositionInRiver, Random.Range(minimumZInRiver, maximumZInRiver)),
                    fishPrefabs[fishIndex].transform.rotation); // Instantiate fishPrefab at fishIndex at new Vector3
                spawnedFishInRiverCount++; // spawnedFishInRiverCount equals itself plus 1
            }
        }
    }

    // Method to call when trash is destroyed
    private void OnTrashDestroyed()
    {
        // If spawnedTrashInRiverCount is greater than zero...
        if (spawnedTrashInRiverCount > 0)
        {
            spawnedTrashInRiverCount--; // spawnedTrashInRiverCount is equal to itself minus 1
        }
    }

    // Method to call when fish are destroyed
    public void OnFishDestroyed()
    {
        // If spawnedFishInRiverCount is greater than zero...
        if (spawnedFishInRiverCount > 0)
        {
            spawnedFishInRiverCount--; // spawnedFishInRiverCount is equal to itself minus 1
        }
    }

    // Method to call when test tube is destroyed
    public void OnTestTubeDestroyed()
    {
        // If spawnedTestTubeCount is greater than zero...
        if (spawnedTestTubeCount > 0)
        {
            spawnedTestTubeCount--; // spawnedTestTubeCount is equal to itself minus 1
        }
    }

    private Vector3 GetRandomNavMeshPosition(Vector3 centerPoint, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius; // Get a random direction within the radius
        randomDirection += centerPoint; // Offset by the specified center point

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position; // Return the valid position on the NavMesh
        }

        return Vector3.zero; // Return zero if no valid position is found
    }
}
