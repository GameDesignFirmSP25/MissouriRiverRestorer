using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FishInvasiveAi : MonoBehaviour
{
    [Header("Game Objects")]
    private List<GameObject> cachedTargets;
    private Transform newTarget;

    [Header("Booleans")]
    private bool isMoving = false;

    [Header("Float Variables")]
    private float speed = 25f;
    private float targetReachedDistance = 1.0f;

    [Header("Integer Variables")]
    private int cachedHash;

    void Start()
    {
        // If AnimalCollectionSpawnManager.Instance or fishList is not null...
        if (AnimalCollectionSpawnManager.Instance != null && AnimalCollectionSpawnManager.Instance.fishList != null)
        {
            cachedTargets = new List<GameObject>(AnimalCollectionSpawnManager.Instance.fishList); // Initialize cachedTargets with the fishList from AnimalGameManager
        }
        else
        {
            Debug.LogError("AnimalCollectionSpawnManager.Instance or fishList is null. Cannot initialize targets."); // Debug.Log
            return; // Return to avoid null reference exception
        }

        cachedTargets.RemoveAll(t => t == null); // Remove any null entries from the cachedTargets list

        // If cachedTargets is null or empty...
        if (cachedTargets.Count == 0)
        {
            Debug.LogWarning("No target positions available for the fish."); // Debug.Log
            return; // Return to avoid null reference exception
        }

        cachedHash = CalculateListHash(cachedTargets); // Calculate the hash of the initial list
        SetNewTarget(); // Set the initial target
    }

    void Update()
    {
        // If AnimalGameManager,Instance or fishList is null...
        if (AnimalCollectionSpawnManager.Instance == null || AnimalCollectionSpawnManager.Instance.fishList == null)
        {
            Debug.LogError("AnimalCollectionSpawnManager.Instance or fishList is null during Update."); // Debug.Log
            return; // Return to avoid null reference exception
        }

        // If cachedTargets is null or empty...
        if (cachedTargets == null || cachedTargets.Count == 0)
        {
            Debug.LogWarning("No target positions available for the fish."); // Debug.Log
            return; // Return to avoid null reference exception
        }

        // If newTarget is null or empty...
        if (newTarget != null && Vector3.Distance(transform.position, newTarget.position) < targetReachedDistance)
        {
            OnTargetReached(); // Call OnTargetReached method when the target is reached
        }

        // If isMoving is false...
        if (newTarget != null)
        {
            MoveTowardsTarget(); // Call MoveTowardsTarget method to move the fish towards the target
        }

        // If hasListChanged() is true...
        if (HasListChanged())
        {
            Debug.Log("The targets list has changed."); // Debug.Log
            cachedTargets = new List<GameObject>(AnimalCollectionSpawnManager.Instance.fishList); // Update cachedTargets with the new fishList
        }
    }

    // Method to set a new target for the fish
    private void SetNewTarget()
    {
        GameObject selectedTarget = cachedTargets[Random.Range(0, cachedTargets.Count)]; // Select a random target from the cachedTargets list

        // If selectedTarget is null...
        if (selectedTarget == null)
        {
            Debug.LogWarning("Selected target GameObject is null."); // Debug.Log
            return; // Return to avoid null reference exception
        }

        newTarget = selectedTarget.transform; // Set the new target to the selected target's transform
        isMoving = true; // Set bool isMoving to true
    }

    // Method to move the fish towards the target
    private void MoveTowardsTarget()
    {
        RotateFish(); // Call RotateFish method to rotate the fish towards the target
        transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime); // Move the fish towards the target position

        // If the distance between the fish and the target is less than targetReachedDistance...
        if (Vector3.Distance(transform.position, newTarget.position) < targetReachedDistance)
        {
            isMoving = false; // Set bool isMoving to false
        }
    }

    // Method to rotate the fish towards the target position
    private void RotateFish()
    {
        // If newTarget is null...
        if (newTarget != null)
        {
            Vector3 direction = newTarget.position - transform.position; // Calculate the direction from the fish to the target
            Quaternion targetRotation = Quaternion.LookRotation(direction); // Calculate the target rotation based on the direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed); // Smoothly rotate the fish towards the target rotation
        }
    }

    // Method to be called when the target is reached
    void OnTargetReached()
    {
        isMoving = false; // Set bool isMoving to false
        SetNewTarget(); // Call SetNewTarget method to set a new target for the fish
    }

    // Method to check if the list of targets has changed
    private bool HasListChanged()
    {
        int currentHash = CalculateListHash(AnimalCollectionSpawnManager.Instance.fishList); // Calculate the hash of the current fishList

        // If cachedHash is not equal to currentHash...
        if (cachedHash != currentHash)
        {
            cachedHash = currentHash; // Update cachedHash with the new hash
            return true; // Return true to indicate that the list has changed
        }
        return false; // Return false to indicate that the list has not changed
    }

    // Method to calculate the hash of a list of GameObjects
    private int CalculateListHash(List<GameObject> list)
    {
        int hash = 17; // Initialize hash with a prime number

        // Iterate through each item in the list
        foreach (var item in list)
        {
            hash = hash * 31 + (item != null ? item.GetHashCode() : 0); // Calculate the hash for each item in the list
        }
        return hash; // Return the final hash value
    }
}

