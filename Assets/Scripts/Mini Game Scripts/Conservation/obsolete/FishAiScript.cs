using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishAiScript : MonoBehaviour
{
    [Header("Game Objects")]
    //public GameObject[] nativePrefab;
    private GameObject[] targetPosition;
    private Transform newTarget;
    public Transform spawnArea;

    [Header("Booleans")]
    bool isMoving = false;
    public static bool spawnNewFish = false; // Flag to indicate if a new fish should be spawned

    [Header("Float Variables")]
    private float speed;
    private float speedMin = 15.0f;
    private float speedMax = 30.0f;
    private float yPosition = -2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = new GameObject[15];
        targetPosition[0] = GameObject.Find("Waypoint 1");
        targetPosition[1] = GameObject.Find("Waypoint 2");
        targetPosition[2] = GameObject.Find("Waypoint 3");
        targetPosition[3] = GameObject.Find("Waypoint 4");
        targetPosition[4] = GameObject.Find("Waypoint 5");
        targetPosition[5] = GameObject.Find("Waypoint 6");
        targetPosition[6] = GameObject.Find("Waypoint 7");
        targetPosition[7] = GameObject.Find("Waypoint 8");
        targetPosition[8] = GameObject.Find("Waypoint 9");
        targetPosition[9] = GameObject.Find("Waypoint 10");
        targetPosition[10] = GameObject.Find("Waypoint 11");
        targetPosition[11] = GameObject.Find("Waypoint 12");
        targetPosition[12] = GameObject.Find("Waypoint 13");
        targetPosition[13] = GameObject.Find("Waypoint 14");
        targetPosition[14] = GameObject.Find("Waypoint 15");


        //spawnArea = GameObject.Find("SpawnArea")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // If targetPosition is null or empty...
        if (targetPosition == null || targetPosition.Length == 0)
        {
            Debug.LogWarning("No target positions available for the native fish."); // Debug.Log
            return; // Return early to avoid errors
        }

        // If bool isMoving is false...
        if (!isMoving)
        {
            GameObject target = targetPosition[Random.Range(0, targetPosition.Length)]; // Randomly select a target position from the array

            // If target is not null...
            if (target != null)
            {
                Vector3 targetPosition = target.transform.position; // Get the current position
                targetPosition.y = yPosition; // Set the y position to yPosition
                target.transform.position = targetPosition; // Assign the modified position back

                newTarget = target.transform; // Set newTarget to the target's transform
                isMoving = true; // Set bool isMoving to true
            }
        
        }

        // If newTarget is not null...
        if (newTarget != null)
        { 
            RotateFish(); // Call RotateFish method to rotate the fish towards the target position
            speed = Random.Range(speedMin, speedMax); //set the speed of the fish
            Vector3 targetPosition = newTarget.position; // Get the target position from newTarget
            targetPosition.y = yPosition; // Set the y position to yPosition
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // Move the fish towards the target position

            Debug.Log($"Fish moving towards {newTarget.name}. Current position: {transform.position}, Target position: {targetPosition}");

            // If the fish has reached the target position...
            if (Vector3.Distance(transform.position, newTarget.position) < 0.5f)
            {
                isMoving = false; // Set bool isMoving to false
                Debug.Log("Fish reached the target.");
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
            direction.y = 0; // Set the y component to 0 to keep the fish level
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
            Debug.Log("Native fish " + gameObject + " destroyed."); // Debug.Log
            spawnNewFish = true; // Set the flag to spawn a new fish
            AnimalCollectionSpawnManager.DecrementSpawnedNativeFish(); // Decrement the count of spawned native fish
        }
    }
}
