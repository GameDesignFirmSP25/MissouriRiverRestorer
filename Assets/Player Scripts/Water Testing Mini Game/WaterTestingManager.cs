using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTestingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject animalPrefab;

    [SerializeField]
    private float minimumSpawnTime;

    [SerializeField]
    private float maximumSpawnTime;

    private bool isPressed = false;
    public bool waterCollected = false;

    public Raycast riverScript;
    public ProgressBar progressBarScript;

    private float startTime;
    private float endTime;
    private float pressDuration;
    private float totalTime;
    private float timeUntilSpawn;
    public float minXPosition = -140f; // first X coordinate for spawning
    public float maxXPosition = 140f; // second X coordinate for spawning
    public float yPosition = 5f;
    public float minZPosition = 15f; // minimum Z coordinate for spawning
    public float maxZPosition = 90f; // maximum Z coordinate for spawning

    void Awake()
    {
        SetTimeUntilSpawn();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Set Cursor to be visible
        Cursor.visible = true;
    }

    // Update is called once per frame
    public void Update()
    {
        riverScript.RiverClicked();
        
        if (riverScript.riverClicked) 
        {
            CalculateTime();
            // riverScript.riverClicked = false;
        }

        timeUntilSpawn -= Time.deltaTime;

        if (timeUntilSpawn <= 0)
        {
            // Generate random X and Z coordinates
            float randomXPosition = Random.Range(minXPosition, maxXPosition);
            float randomZPosition = Random.Range(minZPosition, maxZPosition);

            // Instantiate the NPC at the random position
            Instantiate(animalPrefab, new Vector3(randomXPosition, yPosition, randomZPosition), Quaternion.identity);
            SetTimeUntilSpawn();
        }
    }

    public void CalculateTime()
    {
        if (Input.GetMouseButtonDown(0) && !isPressed) // When left mouse button is down and isPressed is false...
        {
            waterCollected = true;
            isPressed = true;
            startTime = Time.time; // startTime is stored
            InvokeRepeating("InvokeProgressBar", 0f , 1.0f); // add progress to bar every second 
        }

        if (Input.GetMouseButtonUp(0) && isPressed) // When left mouse button is down and isPressed is true...
        {
            endTime = Time.time; // endTime is stored
            pressDuration = endTime - startTime; // measures how long the mouse was pressed for each click
            totalTime = totalTime + pressDuration; // total amount of time mouse was pressed
            Debug.Log("Press Duration: " + pressDuration + "seconds"); // Calculate duration of each mouse button press
            Debug.Log("Total Time Pressed: " + totalTime + "seconds"); // Calculate total time mouse button was pressed
            isPressed = false;
            waterCollected = false;
            riverScript.riverClicked = false;
            CancelInvoke(); // cancel all invokes
        }
    }
    // Invoke function that add to progress bar
    public void InvokeProgressBar()
    {
        progressBarScript.IncrementProgress(0.1f);
    }

    // set time for animals to spawn
    private void SetTimeUntilSpawn()
    {
        timeUntilSpawn = Random.Range(minimumSpawnTime, maximumSpawnTime);
    }
}

