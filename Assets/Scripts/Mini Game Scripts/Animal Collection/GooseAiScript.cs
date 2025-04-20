using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GooseAiScript : MonoBehaviour
{
    [Header("Game Objects")]
    //public GameObject goosePrefab;
    public GameObject[] targetPosition;
    private Transform newTarget;
    //public Transform spawnArea;

    [Header("Booleans")]
    bool isMoving = false;

    [Header("Float Variables")]
    private float speed;
    private float speedMin = 5.0f;
    private float speedMax = 20.0f;
    private float yPosition = -1;

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
            Debug.LogWarning("No target positions available for the fish."); // Debug.Log
            return; // Return early to avoid errors
        }

        // If bool isMoving is false...
        if (!isMoving)
        {
            GameObject target = targetPosition[Random.Range(0, targetPosition.Length)]; // Get a random target position from the array
            
            // If target is not null...
            if (target != null)
            {
                Vector3 targetPosition = target.transform.position; // Get the current position
                targetPosition.y = yPosition; // Set the y position to yPosition
                target.transform.position = targetPosition; // Assign the modified position back

                newTarget = target.transform; // Set newTarget to the transform of the target GameObject
                isMoving = true; // Set bool isMoving to true
            }
        }
        
        // If newTarget is not null...
        if (newTarget != null)
        {
            RotateGoose(); // Call the method to rotate the goose towards the target position
            speed = Random.Range(speedMin, speedMax); //set the speed of the goose
            transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime); // Move the goose towards the target position
            Vector3 newPosition = transform.position; // Get the current position of the goose
            newPosition.y = yPosition; // Set the y position to yPosition
            transform.position = newPosition; // Update the position of the goose with the new y value

            // If the goose has reached the target position...
            if (transform.position == newTarget.position)
            {
                isMoving = false; // Set bool isMoving to false
            }
        }
    }

    // Method to rotate the goose towards the target position
    void RotateGoose()
    {
        // If newTarget is not null...
        if (newTarget != null)
        {
            Vector3 direction = newTarget.position - transform.position; // Calculate the direction vector from the goose to the target position
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Create a rotation that looks in the direction of the target position
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime); // Smoothly rotate the goose towards the target position
        }
    }
}
