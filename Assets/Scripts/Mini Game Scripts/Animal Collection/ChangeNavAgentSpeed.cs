using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.LowLevel;

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
    private bool speedUpdated = false; // Track if the agent speed has been changed

    void Start()
    {
        // Make sure the agent component is assigned in the Inspector
        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
        }


        agent.stoppingDistance = 1f; // Set a small stopping distance

        
        
        SetAgentSpeed(0); ; // Stop the agent
        
       
    }

    void Update()
    {
        timer += Time.deltaTime; // Increment the timer

        // If the deer event is not complete, stop the agent
        if (AnimalGameManager.deerEventZoneComplete && !speedUpdated)
        {
            SetAgentSpeed(3.5f); // Set walking speed
        }

        AnimalMovement(); // Call the animal movement logic

        SetPosition();
    }

    // Method to set the agent's position to a random point within a sphere
    private void SetPosition()
    {
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

    public void SetAgentSpeed(float speed)
    {
        if (agent != null)
        {
            speedUpdated = true; // Mark that the speed has been updated
            agent.speed = speed; // Update the agent's speed
            Debug.Log($"Agent speed set to {speed} for {gameObject.name}");
            AnimalMovement(); // Call the movement logic to ensure it reacts to the speed change
            SetPosition(); // Set a new position based on the updated speed
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is not assigned.");
        }
    }
}
