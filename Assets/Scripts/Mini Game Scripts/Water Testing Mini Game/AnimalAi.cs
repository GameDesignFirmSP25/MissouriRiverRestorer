using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AnimalAi : MonoBehaviour
{
    public List<GameObject> destinationPoints;

    private GameObject randomDestinationPoint;

    private NavMeshAgent agent;

    public float radius;
    private float waitTime = 10f;
    private float waitCounter = 0f;

    public bool isWaiting = false;
    public bool isWalking = false;

    private void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

        // If agent is null...
        if (agent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null."); // Debug.Log error 
        }

        SetDestinationPoint();
        isWalking = true; // Set bool isWalking to true
    }

    private void Update()
    {
        AnimalMovement();
    }

    // Set Destination point
    private void SetDestinationPoint()
    {
        int randomIndex = Random.Range(0, destinationPoints.Count); // Get a random index

        GameObject randomDestinationPoint = destinationPoints[randomIndex]; // Access the destination point at the random index
        Debug.Log("Random Destination Point: " + randomDestinationPoint.transform.position); // Debug.Log the position of the random destination point

        agent.SetDestination(randomDestinationPoint.transform.position); // Set Destination point
    }

    // Controls animal movement
    private void AnimalMovement()
    {
        // If bool isWalking is true...
        if (isWalking)
        {
            waitCounter = 0f; // Set wait counter back to 0
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

        // If bool isWalking and isWaiting are both false...
        if (!isWalking && !isWaiting)
        {
            SetDestinationPoint();
            isWalking = true; // Set bool isWalking to true
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}