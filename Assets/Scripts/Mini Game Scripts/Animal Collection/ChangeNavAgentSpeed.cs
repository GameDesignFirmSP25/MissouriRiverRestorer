using UnityEngine;
using UnityEngine.AI;

public class ChangeNavAgentSpeed : MonoBehaviour
{
    public NavMeshAgent agent;

    //public AnimalGameManager animalGameManager;

    public float radius;
    private float timer;
    private float waitTime = 8f;
    private float waitCounter = 0f;
    private float wanderRadius = 40f;
    private float wanderTimer = 10f;

    public bool isWaiting = true;
    public bool isWalking = false;

    void Start()
    {
        // Make sure the agent component is assigned in the Inspector
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }


        agent.stoppingDistance = 1f; // Set a small stopping distance
    }

    void Update()
    {
        //timer += Time.deltaTime; // timer is equal to itslef plus Time.deltaTime 

        //// If timer is greater than or equal to wanderTimer...
        //if (timer >= wanderTimer)
        //{
        //    Debug.Log("Timer reached. Setting new destination."); // Debug.Log
        //    Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1); // newPos is equal to 
        //    agent.SetDestination(newPos); // Set destination of agent to newPos
        //    timer = 0; // Set timer to 0
        //    isWalking = true; // Set bool isWalking to true
        //    isWaiting = false; // Set bool isWaiting to false
        //    Debug.Log("isWalking set to true.");
        //    AnimalMovement();
        //}

        //// Change the speed based on some condition (e.g., a button press)
        //if (!AnimalGameManager.deerEventZoneComplete)
        //{
        //    agent.speed = 0f; // Increase speed
        //}
        //else if (AnimalGameManager.deerEventZoneComplete)
        //{
        //    agent.speed = 3.5f;  // Reduce speed
        //}
        timer += Time.deltaTime; // Increment the timer

        // If the deer event is not complete, stop the agent
        if (!AnimalGameManager.deerEventZoneComplete)
        {
            agent.speed = 0f; // Stop the agent
        }
        else if (AnimalGameManager.deerEventZoneComplete)
        {
            agent.speed = 3.5f; // Set walking speed
            AnimalMovement(); // Trigger the movement logic
        }

        // Handle wandering logic if the timer exceeds the wander time
        if (timer >= wanderTimer && isWalking)
        {
            Debug.Log("Timer reached. Setting new destination.");
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0; // Reset the timer
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
        //// If bool isWalking is true...
        //if (isWalking)
        //{
        //    Debug.Log("Animal is walking. Transitioning to waiting state."); // Debug.Log
        //    isWalking = false; // Set bool isWalking to false
        //    isWaiting = true; // Set bool isWaiting to true
        //    //agent.speed = 0f; // Set the speed of the NavMeshAgent to 0

        //}

        //// If bool isWaiting is true...
        //if (isWaiting)
        //{
        //    Debug.Log("Animal is waiting."); // Debug.Log the state of isWaiting
        //    waitCounter += Time.deltaTime; // Wait counter is equal to wait counter plus Time.deltaTime
        //    if (waitCounter < waitTime) // If wait counter is less than wait time...
        //        return;
        //    Debug.Log("Wait time completed. Transitioning to walking state."); // Debug.Log
        //    isWaiting = false; // Set bool isWaiting to false
        //    //agent.speed = 3.5f; // Set the speed of the NavMeshAgent to 3.5
        //    waitCounter = 0f; // Reset wait counter to 0
        //}
        // If the deer event is complete, ensure the deer starts walking
        // If the deer event is complete, ensure the deer starts walking
        if (AnimalGameManager.deerEventZoneComplete && !isWalking)
        {
            Debug.Log("Deer event complete. Starting to walk immediately.");
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1); // Get a new random position
            agent.SetDestination(newPos); // Set the destination
            isWalking = true; // Set walking state
            isWaiting = false; // Ensure waiting is false
            return; // Exit the method to avoid further checks
        }

        // If the deer is walking, check if it has reached its destination
        if (isWalking)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                Debug.Log("Deer reached destination. Transitioning to waiting state.");
                isWalking = false; // Set walking to false
                isWaiting = true; // Set waiting to true
            }
        }

        // If the deer is waiting, handle the waiting logic
        if (isWaiting)
        {
            Debug.Log("Deer is waiting.");
            waitCounter += Time.deltaTime; // Increment the wait counter
            if (waitCounter < waitTime) // If the wait time is not yet complete
                return;

            Debug.Log("Wait time completed. Transitioning to walking state.");
            isWaiting = false; // Set waiting to false
            isWalking = true; // Set walking to true
            waitCounter = 0f; // Reset the wait counter

            // Set a new destination
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
        }
    }
}
