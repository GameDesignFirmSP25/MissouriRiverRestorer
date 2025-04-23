using UnityEngine;
using UnityEngine.AI;

public class FishScript : MonoBehaviour
{
    private AnimalGameManager gameManager;

    private NavMeshAgent agent;
    private Transform target;

    public float radius;
    private float timer;
    private float waitTime = 7f;
    private float waitCounter = 0f;
    private float wanderRadius = 40f;
    private float wanderTimer = 15f;

    public bool isWaiting = true;
    public bool isWalking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component from the game object
        timer = wanderTimer; // Set timer to wanderTimer

        // If agent is null...
        if (agent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null."); // Debug.Log error 
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // timer is equal to itslef plus Time.deltaTime 

        // If timer is greater than or equal to wanderTimer...
        if (timer >= wanderTimer)
        {
            Debug.Log("Timer reached. Setting new destination."); // Debug.Log
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1); // newPos is equal to 
            agent.SetDestination(newPos); // Set destiantion of agent to newPos
            timer = 0; // Set timer to 0
            isWalking = true; // Set bool isWalking to true
            isWaiting = false; // Set bool isWaiting to false
            FishMovement();
        }
    }

    // Static method to get a random position on the NavMesh within a sphere around the origin
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist; // Get a random direction within a sphere of radius dist

        randDirection += origin; // Add the origin to the random direction to get a new position

        NavMeshHit navHit; // Create a NavMeshHit variable to store the hit information

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask); // Sample the NavMesh at the random position to find a valid point on the NavMesh

        return navHit.position; // Return the position of the NavMesh hit
    }

    // Controls fish movement
    private void FishMovement()
    {
        // If bool isWalking is true...
        if (isWalking)
        {
            isWalking = false; // Set bool isWalking to true
            isWaiting = true; // Set bool isWaiting to false
        }

        // If bool isWalking is true...
        if (isWaiting)
        {
            waitCounter += Time.deltaTime; // Wait counter is equal to wait counter plus Time.deltaTime
            if (waitCounter < waitTime) // If wait counter is less than wait time...
                return;
            isWaiting = false; // Set bool isWaiting to false
        }
    }
}
