using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishAiScript : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject[] nativePrefab;
    public GameObject[] targetPosition;
    private Transform newTarget;
    public Transform spawnArea;

    [Header("Booleans")]
    bool isMoving = false;

    [Header("Float Variables")]
    private float speed;
    private float speedMin = 15.0f;
    private float speedMax = 30.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = new GameObject[10];
        targetPosition[0] = GameObject.Find("Waypoint 1");
        targetPosition[1] = GameObject.Find("Waypoint 2");
        targetPosition[2] = GameObject.Find("Waypoint 3");
        targetPosition[3] = GameObject.Find("Waypoint 4");
        targetPosition[4] = GameObject.Find("Waypoint 5");
        targetPosition[0] = GameObject.Find("Waypoint 6");
        targetPosition[1] = GameObject.Find("Waypoint 7");
        targetPosition[2] = GameObject.Find("Waypoint 8");
        targetPosition[3] = GameObject.Find("Waypoint 9");
        targetPosition[4] = GameObject.Find("Waypoint 10");

        spawnArea = GameObject.Find("SpawnArea")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If targetPosition is null or empty...
        if (targetPosition == null || targetPosition.Length == 0)
        {
            Debug.LogWarning("No target positions available for the fish."); // Debug.Log
            return; // Return early to avoid errors
        }

        // If bool isMoving is false...
        if (!isMoving)
        {
            GameObject target = targetPosition[Random.Range(0, targetPosition.Length)]; // Randomly select a target position from the array

            // If target is not null...
            if (target != null)
            {
                newTarget = target.transform; // Set newTarget to the transform of the selected target position
                isMoving = true; // Set bool isMoving to true
            }
        }

        // If newTarget is not null...
        if (newTarget != null)
        { 
            RotateFish(); // Call RotateFish method to rotate the fish towards the target position
            speed = Random.Range(speedMin, speedMax); //set the speed of the fish
            transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime); // Move the fish towards the target position at the specified speed

            // If the fish has reached the target position...
            if (transform.position == newTarget.position)
            {
                isMoving = false; // Set bool isMoving to false
            }
        }
    }

    // Method to rotate the fish towards the target position
    void RotateFish()
    {
        // If newTarget is not null...
        if (newTarget != null)
        {
            Vector3 direction = newTarget.position - transform.position; // Calculate the direction vector from the fish to the target position
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Create a rotation that looks in the direction of the target position
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime); // Smoothly rotate the fish towards the target position
        }
    }

    // Destroy the fish when it collides with an invasive fish
    void OnTriggerEnter(Collider other)
    {
        // If the other object has the tag "Invasive"...
        if (other.CompareTag("Invasive"))
        {
            Destroy(gameObject); // Destroy the fish game object
            Debug.Log("Native fish" + gameObject + " destroyed."); // Debug.Log
            AnimalCollectionSpawnManager.DecrementSpawnedNativeFish(); // Decrement the count of spawned native fish
        }
    }

    // Method called when the fish is destroyed
    void OnDestroy()
    {
        // If the nativePrefab array is null or empty...
        if (nativePrefab == null || nativePrefab.Length == 0)
        {
            Debug.LogError("nativePrefab array is empty or not assigned. Cannot instantiate fish."); // Debug.Log
            return; // Return early to avoid errors
        }

        // If AnimalGameManager.Instance is null...
        if (AnimalGameManager.Instance == null)
        {
            Debug.LogError("AnimalGameManager.Instance is null. Cannot add the new fish to the list."); // Debug.Log
            return; // Return early to avoid errors
        }

        int fishIndex = Random.Range(0, nativePrefab.Length); // Randomly select a fish prefab from the nativePrefab array

        Vector3 spawnPosition = spawnArea != null ? spawnArea.position : transform.position; // Use the spawn area position if available, otherwise use the current position of the fish

        GameObject newFish = Instantiate(nativePrefab[fishIndex], spawnPosition, Quaternion.identity); // Instantiate the new fish at the spawn position with no rotation

        newFish.tag = "Native"; // Set the tag of the new fish to "Native"

        AnimalGameManager.Instance.AddToFishList(newFish); // Add the new fish to the list of fish in AnimalGameManager

        Debug.Log($"New fish instantiated at {newFish.transform.position} and added to list: {newFish.name}"); // Debug.Log

        AnimalCollectionSpawnManager.IncrementSpawnedNativeFish(); // Increment the count of spawned native fish
    }
}
