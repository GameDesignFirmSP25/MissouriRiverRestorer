using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishAiScript : MonoBehaviour
{
    public GameObject nativePrefab;
    public GameObject[] targetPosition;
    private Transform newTarget;
    public Transform spawnArea;

    bool isMoving = false;

    private float speed;
    private float speedMin = 15.0f;
    private float speedMax = 30.0f;

    public bool predator = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = new GameObject[5];
        targetPosition[0] = GameObject.Find("Waypoint 1");
        targetPosition[1] = GameObject.Find("Waypoint 2");
        targetPosition[2] = GameObject.Find("Waypoint 3");
        targetPosition[3] = GameObject.Find("Waypoint 4");
        targetPosition[4] = GameObject.Find("Waypoint 5");

        spawnArea = GameObject.Find("SpawnArea")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetPosition == null || targetPosition.Length == 0)
        {
            Debug.LogWarning("No target positions available for the fish.");
            return;
        }

        if (!isMoving)
        {
            GameObject target = targetPosition[Random.Range(0, targetPosition.Length)];
            if (target != null)
            {
                newTarget = target.transform;
                isMoving = true;
            }
        }

        if (newTarget != null)
        {
            RotateFish();
            speed = Random.Range(speedMin, speedMax); //set the speed of the fish
            transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime);

            if (transform.position == newTarget.position)
            {
                isMoving = false;
            }
        }
    }

    // Method to rotate the fish towards the target position
    void RotateFish()
    {
        if (newTarget != null)
        {
            Vector3 direction = newTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    // Destroy the fish when it collides with an invasive fish
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Invasive"))
        {
            Destroy(gameObject);
            Debug.Log("Native fish" + gameObject + " destroyed.");
        }
    }

    // Method to spawn a new fish at random position
    void SpawnNewFish()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f)) + spawnArea.position;
        GameObject newFish = Instantiate(nativePrefab, spawnPosition, Quaternion.identity);
        newFish.GetComponent<FishAiScript>().enabled = true; // Enable the script on the new fish
    }

    private void OnDestroy()
    {
        SpawnNewFish();
        Debug.Log("New fish spawned.");

    }
}
