using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class AnimalAi : MonoBehaviour
{
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

    private void Start()
    {
        // Get the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;

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
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1); // newPos is equal to 
            agent.SetDestination(newPos); // Set destiantion of agent to newPos
            timer = 0; // Set timer to 0
            isWalking = true; // Set bool isWalking to true
            isWaiting = false; // Set bool isWaiting to false
            AnimalMovement();
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    // Controls animal movement
    private void AnimalMovement()
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

#if UNITY_EDITOR

    private void OnDrawGizmos ()
    {
        Gizmos.DrawWireSphere (transform.position, radius);
    }

#endif
}