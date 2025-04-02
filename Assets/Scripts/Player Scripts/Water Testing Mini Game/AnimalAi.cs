using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        if (agent == null)
        {
            Debug.LogError("Nav Mesh Agent is Null.");
        }

        SetDestinationPoint();
        isWalking = true;
    }

    private void Update()
    {
        AnimalMovement();
    }

    private void SetDestinationPoint()
    {
        // Get a random index
        int randomIndex = Random.Range(0, destinationPoints.Count);

        // Access the destination point at the random index
        GameObject randomDestinationPoint = destinationPoints[randomIndex];
        Debug.Log("Random Destination Point: " + randomDestinationPoint.transform.position);

        // Set Destination point
        agent.SetDestination(randomDestinationPoint.transform.position);
    }

    private void AnimalMovement()
    {
        // if bool isWalking is true...
        if (isWalking)
        {
            waitCounter = 0f; // Set wait counter back to 0
            isWalking = false; // Set bool isWalking to true
            isWaiting = true; // Set bool isWaiting to false
        }

        // if bool isWalking is true...
        if (isWaiting)
        {
            waitCounter += Time.deltaTime; // Wait counter is equal to wait counter plus Time.deltaTime
            if (waitCounter < waitTime) // if wait counter is less than wait time...
                return;
            isWaiting = false; // set bool isWaiting to false
        }

        if (!isWalking && !isWaiting)
        {
            SetDestinationPoint();
            isWalking = true;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}