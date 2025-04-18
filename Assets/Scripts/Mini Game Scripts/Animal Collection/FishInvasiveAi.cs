using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FishInvasiveAi : MonoBehaviour
{
    private List<GameObject> target; 
    private Transform newTarget; 

    private bool isMoving = false;

    private float speed = 25f;
    //private float speedMin = 15.0f;
    //private float speedMax = 30.0f;

    [SerializeField] private float targetReachedDistance = 1.0f; // Distance threshold to consider the target reached

    void Start()
    {
        target = new List<GameObject>(); // 

        // Dynamically find and add GameObjects to the target list
        for (int i = 0; i < 15; i++)
        {
            GameObject fishTarget = GameObject.Find($"Fish_ Pallid Sturgeon ({i})") ?? GameObject.Find($"Fish_ Pallid Sturgeon(Clone)");

            if (fishTarget != null)
            {
                target.Add(fishTarget); // Add the GameObject to the list
                Debug.Log("New fish added to list.");
            }
            else
            {
                Debug.LogWarning($"Fish_ Pallid Sturgeon ({i}) not found or Fish_ Pallid Sturgeon(Clone) not found.");
            }
        }

        target.RemoveAll(t => t == null);

        if (target == null || target.Count == 0)
        {
            Debug.LogWarning("No target positions available for the fish.");
            return;
        }

        // Select an initial random target
        SetNewTarget();
    }

    void Update()
    {
        if (target == null || target.Count == 0)
        {
            Debug.LogWarning("No target positions available for the fish.");
            return;
        }

        if (newTarget != null && Vector3.Distance(transform.position, newTarget.position) < targetReachedDistance)
        {
            // Target reached! Switch to the next target or another behavior
            OnTargetReached();
        }

        if (newTarget != null)
        {
            MoveTowardsTarget();
        }
    }

    private void SetNewTarget()
    {
        // Select a random target from the array
        GameObject selectedTarget = target[Random.Range(0, target.Count)];
        if (selectedTarget == null)
        {
            Debug.LogWarning("Selected target GameObject is null.");
            return;
        }

        newTarget = selectedTarget.transform; // Assign the Transform of the selected GameObject
        isMoving = true;
        //speed = Random.Range(speedMin, speedMax); // Set a random speed
    }

    private void MoveTowardsTarget()
    {
        RotateFish();
        transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, newTarget.position) < targetReachedDistance)
        {
            isMoving = false; // Stop moving once the target is reached
        }
    }

    private void RotateFish()
    {
        if (newTarget != null)
        {
            Vector3 direction = newTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }
    }

    void OnTargetReached()
    {
        // Handle what happens when the target is reached
        // For example, you can set a new target or change the fish's behavior
        isMoving = false;
        SetNewTarget();
    }
}

