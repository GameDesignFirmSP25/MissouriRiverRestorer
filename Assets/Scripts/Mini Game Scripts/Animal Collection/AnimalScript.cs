using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AnimalScript : MonoBehaviour
{
    private AnimalGameManager gameManager;

    private NavMeshAgent agent;
    private Transform target;

    public float radius;
    private float timer;
    private float waitTime = 10f;
    private float waitCounter = 0f;
    private float wanderRadius = 40f;
    private float wanderTimer = 10f;

    public bool isWaiting = true;
    public bool isWalking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component from the game object
        timer = wanderTimer; // Set timer to wanderTimer
        //agent.speed = 0f; // Set the speed of the NavMeshAgent to 0

        // If agent is null...
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing.");
            return;
        }

        if (!agent.isOnNavMesh)
        {
            Debug.LogError("NavMeshAgent is not on the NavMesh.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update method called."); // Debug.Log
        timer += Time.deltaTime; // timer is equal to itslef plus Time.deltaTime 

        // If timer is greater than or equal to wanderTimer...
        if (timer >= wanderTimer)
        {
            Debug.Log("Timer reached. Setting new destination."); // Debug.Log
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1); // newPos is equal to 
            agent.SetDestination(newPos); // Set destination of agent to newPos
            timer = 0; // Set timer to 0
            isWalking = true; // Set bool isWalking to true
            isWaiting = false; // Set bool isWaiting to false
            Debug.Log("isWalking set to true.");
            AnimalMovement();
        }
    }

    // Static method to get a random position on the NavMesh within a sphere around the origin
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist; // Get a random direction within a sphere of radius dist

        randDirection += origin; // Add the origin to the random direction to get a new position

        NavMeshHit navHit; // Create a NavMeshHit variable to store the hit information

        if (NavMesh.SamplePosition(randDirection, out navHit, dist, layermask))
        {
            Debug.Log($"Valid NavMesh position found: {navHit.position}");
            return navHit.position;
        }
        else
        {
            Debug.LogWarning("Failed to find a valid NavMesh position.");
            return origin; // Return the origin if no valid position is found
        }
    }

    // Controls animal movement
    private void AnimalMovement()
    {
        // If bool isWalking is true...
        if (isWalking)
        {
            Debug.Log("Animal is walking. Transitioning to waiting state."); // Debug.Log
            isWalking = false; // Set bool isWalking to false
            isWaiting = true; // Set bool isWaiting to true
            //agent.speed = 0f; // Set the speed of the NavMeshAgent to 0

        }

        // If bool isWaiting is true...
        if (isWaiting)
        {
            Debug.Log("Animal is waiting."); // Debug.Log the state of isWaiting
            waitCounter += Time.deltaTime; // Wait counter is equal to wait counter plus Time.deltaTime
            if (waitCounter < waitTime) // If wait counter is less than wait time...
                return;
            Debug.Log("Wait time completed. Transitioning to walking state."); // Debug.Log
            isWaiting = false; // Set bool isWaiting to false
            //agent.speed = 3.5f; // Set the speed of the NavMeshAgent to 3.5
            waitCounter = 0f; // Reset wait counter to 0
        }
    }

}
