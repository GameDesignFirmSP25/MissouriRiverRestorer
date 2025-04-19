using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalCollectionSpawnManager : MonoBehaviour
{
    [Header("Fauna Prefabs")]
    public GameObject[] mammalPrefabs; // Array of mammal prefabs to spawn
    public GameObject[] invasiveFishPrefabs; // Array of fish prefabs to spawn
    public GameObject[] nativeFishPrefabs;
    //public GameObject[] invasiveBirdPrefabs; // Array of bird prefabs to spawn
    public GameObject[] nativeBirdPrefabs; // Array of bird prefabs to spawn
    public GameObject[] reptileAmphibianPrefabs; // Array of reptile/amphibian prefabs to spawn
    public GameObject[] insectPrefabs; // Array of insect prefabs to spawn
    

    [Header("Waypoints")]
    public Transform spawnArea;
    public GameObject[] fishWaypoints; 

    [Header("Integer Variables")]
    private int numberOfMammalsToSpawn = 12;
    private int numberOfInvasiveFishToSpawn = 8;
    private int numberOfNativeFishToSpawn = 20;
    //private int numberOfInvasiveBirdsToSpawn = 10;
    private int numberOfNativeBirdsToSpawn = 8;
    private int numberOfReptilesAmphibiansToSpawn = 7;
    private int numberOfInsectsToSpawn = 15;
    private int spawnedInvasiveFish = 0;
    private static int spawnedNativeFish = 0;

    [Header("Float Variables")]
    private float spawnTime = 0.05f;
    private float spawnDelay = 2.5f;
    private float radius = 150f;
    private float minimumXOnGround = -90f;
    private float maximumXOnGround = 30f;
    private float yPositionOnGround = 3f;
    private float minimumZOnGround = -80f;
    private float maximumZOnGround = 130f;
    private float yPositionInRiver = -2.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnMammals(); // Call the SpawnMammals method to spawn mammals
        SpawnBirds(); // Call the SpawnBirds method to spawn birds
        SpawnReptilesAmphibians(); // Call the SpawnReptilesAmphibians method to spawn reptiles/amphibians
        SpawnInsects(); // Call the SpawnInsects method to spawn insects
        InvokeRepeating("SpawnFish", spawnTime, spawnDelay); // Repeatedly invoke SpawnFish at specified intervals
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Method that spawns mammals at random positions on the ground
    private void SpawnMammals()
    {
        // For, i equals 0, i is less than numberOfTrashOnGroundToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfMammalsToSpawn; i++)
        {
            int mammalIndex = Random.Range(0, mammalPrefabs.Length); // mammalIndex equals a number with in range of 0 to 2
            Instantiate(mammalPrefabs[mammalIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), mammalPrefabs[mammalIndex].transform.rotation); // Instantiate mammalPrefab at mammalIndex at new Vector3
        }
    }

    // Method that spawns fish at random positions in the river
    private void SpawnFish()
    {
        // If spawnedInvasiveFish is less than numberOfInvasiveFishToSpawn...
        if (spawnedInvasiveFish < numberOfInvasiveFishToSpawn)
        {
            int fishIndex = Random.Range(0, invasiveFishPrefabs.Length); // fishIndex equals a number with in range of 0 to 1
            Instantiate(invasiveFishPrefabs[fishIndex], RandomFishPositionInRiver(), RandomRotation()); // Instantiate invasiveFishPrefab at fishIndex at new Vector3
            spawnedInvasiveFish++; // spawnedFish equals itself plus 1
        }

        // If spawnedNativeFish is less than numberOfNativeFishToSpawn...
        if (spawnedNativeFish < numberOfNativeFishToSpawn)
        {
            int fishIndex = Random.Range(0, nativeFishPrefabs.Length); // fishIndex equals a number with in range of 0 to 1
            Instantiate(nativeFishPrefabs[fishIndex], RandomFishPositionInRiver(), RandomRotation()); // Instantiate nativeFishPrefab at fishIndex at new Vector3
            IncrementSpawnedNativeFish(); // Increment the count of spawned native fish
        }
    }

    // Method that spawns birds at random positions on the ground
    private void SpawnBirds()
    {
        //// For, i equals 0, i is less than numberOfInvasiveBirdsToSpawn; when called i is equal to itself plus 1
        //for (int i = 0; i < numberOfInvasiveBirdsToSpawn; i++)
        //{
        //    int birdIndex = Random.Range(0, invasiveBirdPrefabs.Length); // birdIndex equals a number with in range of 0 to 2
        //    Instantiate(invasiveBirdPrefabs[birdIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
        //        yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), invasiveBirdPrefabs[birdIndex].transform.rotation); // Instantiate birdPrefab at birdIndex at new Vector3
        //}

        // For, i equals 0, i is less than numberOfNativeBirdsToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfNativeBirdsToSpawn; i++)
        {
            int birdIndex = Random.Range(0, nativeBirdPrefabs.Length); // birdIndex equals a number with in range of 0 to 2

            if (birdIndex == 1)
            {
                Instantiate(nativeBirdPrefabs[1], RandomGoosePositionInRiver(), RandomRotation()); // Instantiate birdPrefab at birdIndex at new Vector3 in river
            }
            else
            {
                Instantiate(nativeBirdPrefabs[birdIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                    yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), nativeBirdPrefabs[birdIndex].transform.rotation); // Instantiate birdPrefab at birdIndex at new Vector3
            }     
        }
    }

    // Method that spawns reptiles/amphibians at random positions on the ground
    private void SpawnReptilesAmphibians()
    {
        // For, i equals 0, i is less than numberOfTrashOnGroundToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfReptilesAmphibiansToSpawn; i++)
        {
            int reptileAmphibianIndex = Random.Range(0, reptileAmphibianPrefabs.Length); // reptileAmphibianIndex equals a number with in range of 0 to 2
            Instantiate(reptileAmphibianPrefabs[reptileAmphibianIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), reptileAmphibianPrefabs[reptileAmphibianIndex].transform.rotation); // Instantiate reptileAmphibianPrefab at reptileAmphibianIndex at new Vector3
        }
    }

    // Method that spawns insects at random positions on the ground
    private void SpawnInsects()
    {
        // For, i equals 0, i is less than numberOfTrashOnGroundToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfInsectsToSpawn; i++)
        {
            int insectIndex = Random.Range(0, insectPrefabs.Length); // insectIndex equals a number with in range of 0 to 2
            Instantiate(insectPrefabs[insectIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), insectPrefabs[insectIndex].transform.rotation); // Instantiate insectPrefab at insectIndex at new Vector3
        }
    }

    // Vector3 method to generate a random position in the river
    public Vector3 RandomFishPositionInRiver()
    {
        Vector3 randomFishPosition = spawnArea.position + Random.insideUnitSphere * radius; // Generate a random position within the spawn area
        randomFishPosition = transform.TransformPoint(randomFishPosition * 0.5f); // Transform the random position
        randomFishPosition.y = yPositionInRiver; // Set the y position of the random position
        return randomFishPosition; // Return the random position
    }

    // Vector3 method to generate a random position for the goose in the river
    public Vector3 RandomGoosePositionInRiver()
    {
        Vector3 randomGoosePosition = spawnArea.position + Random.insideUnitSphere * radius; // Generate a random position within the spawn area
        randomGoosePosition = transform.TransformPoint(randomGoosePosition * 0.5f); // Transform the random position
        randomGoosePosition.y = yPositionInRiver; // Set the y position of the random position
        return randomGoosePosition; // Return the random position
    }

    //Method to get Random Rotation on fish
    public Quaternion RandomRotation()
    {
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0); // Create a random rotation with a range of angles
        return randomRotation; // Return the random rotation
    }

    // Method to increment the count of spawned native fish
    public static void IncrementSpawnedNativeFish()
    {
        spawnedNativeFish++; // Increment the count of spawned native fish
        Debug.Log($"Spawned native fish count incremented. Current count: {spawnedNativeFish}"); // Debug.Log
    }

    // Method to decrement the count of spawned native fish
    public static void DecrementSpawnedNativeFish()
    {
        // If spawnedNativeFish is greater than 0...
        if (spawnedNativeFish > 0)
        {
            spawnedNativeFish--; // Decrement the count of spawned native fish
            Debug.Log($"Spawned native fish count decremented. Current count: {spawnedNativeFish}"); // Debug.Log
        }
        else
        {
            Debug.LogWarning("Attempted to decrement spawnedNativeFish below zero."); // Log a warning if trying to decrement below zero
        }
    }
}
