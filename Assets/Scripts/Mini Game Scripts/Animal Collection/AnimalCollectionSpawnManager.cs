using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalCollectionSpawnManager : MonoBehaviour
{
    [Header("Fauna Prefabs")]
    public GameObject[] mammalPrefabs; // Array of mammal prefabs to spawn
    public GameObject[] fishPrefabs; // Array of fish prefabs to spawn
    public GameObject[] birdPrefabs; // Array of bird prefabs to spawn
    public GameObject[] reptileAmphibianPrefabs; // Array of reptile/amphibian prefabs to spawn
    public GameObject[] insectPrefabs; // Array of insect prefabs to spawn
    public GameObject nativeFishPrefab;

    [Header("Waypoints")]
    public Transform spawnArea; // Transform representing the area where fish can spawn
    public GameObject[] fishWaypoints; // Array of waypoints for fish movement

    [Header("Integer Variables")]
    private int numberOfMammalsToSpawn = 7;
    private int numberOfFishToSpawn = 8;
    private int numberOfBirdsToSpawn = 7;
    private int numberOfReptilesAmphibiansToSpawn = 7;
    private int numberOfInsectsToSpawn = 7;
    private int spawnedFish = 0;

    [Header("Float Variables")]
    private float spawnTime = 0.05f;
    private float spawnDelay = 2.5f;
    private float radius = 150f;
    private float minimumXOnGround = -10f;
    private float maximumXOnGround = 30f;
    private float yPositionOnGround = 3f;
    private float minimumZOnGround = -70f;
    private float maximumZOnGround = 130f;
    //private float minimumXInRiver = -282f;
    //private float maximumXInRiver = -282f;
    private float yPositionInRiver = -1.5f;
    //private float minimumZInRiver = -126.5f;
    //private float maximumZInRiver = -61.5f;

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

    // Spawns mammals at random positions on the ground
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

    // Spawns fish at random positions in the river
    private void SpawnFish()
    {
        // If spawnedFishInRiverCount is less than minimumFishInRiver...
        if (spawnedFish < numberOfFishToSpawn)
        {
            int fishIndex = Random.Range(0, fishPrefabs.Length); // fishIndex equals a number with in range of 0 to 3
            Instantiate(fishPrefabs[fishIndex], RandomFishPosition(), RandomFishRotation()); // Instantiate fishPrefab at fishIndex at new Vector3
            spawnedFish++; // spawnedFish equals itself plus 1
        }
    }

    // Spawns birds at random positions on the ground
    private void SpawnBirds()
    {
        // For, i equals 0, i is less than numberOfTrashOnGroundToSpawn; when called i is equal to itself plus 1
        for (int i = 0; i < numberOfBirdsToSpawn; i++)
        {
            int birdIndex = Random.Range(0, birdPrefabs.Length); // birdIndex equals a number with in range of 0 to 2
            Instantiate(birdPrefabs[birdIndex], new Vector3(Random.Range(minimumXOnGround, maximumXOnGround),
                yPositionOnGround, Random.Range(minimumZOnGround, maximumZOnGround)), birdPrefabs[birdIndex].transform.rotation); // Instantiate birdPrefab at birdIndex at new Vector3
        }
    }

    // Spawns reptiles/amphibians at random positions on the ground
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

    // Spawns insects at random positions on the ground
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

    // Method to generate a random position for fish in the river
    public Vector3 RandomFishPosition()
    {
        // Generate a random position for fish in the river
        //Vector3 randomPosition = new Vector3(Random.Range(minimumXInRiver, maximumXInRiver), yPositionInRiver, Random.Range(minimumZInRiver, maximumZInRiver));
        Vector3 randomPosition = spawnArea.position + Random.insideUnitSphere * radius; // Generate a random position within the spawn area
        randomPosition = transform.TransformPoint(randomPosition * 0.5f); // Transform the random position
        randomPosition.y = yPositionInRiver; // Set the y position of the random position
        return randomPosition;
    }

    //Method to get Random Rotation on fish
    public Quaternion RandomFishRotation()
    {
        // Generate a random rotation for fish
        Quaternion randomRotation = Quaternion.Euler(Random.Range(-20, 20), Random.Range(0, 360), 0);
        return randomRotation;
    }

    // Method to generate random waypoint

    public Vector3 RandomFishWaypoint()
    {
        int waypointIndex = Random.Range(0, (fishWaypoints.Length - 1)); // Generate a random waypoint index
        Vector3 randomWaypoint = fishWaypoints[waypointIndex].transform.position; // Transform the random waypoint
        return randomWaypoint;
    }
}
