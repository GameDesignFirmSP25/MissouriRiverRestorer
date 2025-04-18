using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishScript : MonoBehaviour
{
    //private float RightBoundry = 60; //how far right the animal despawns
    private float speed;
    private float speedMin = 15.0f; //how fast the animal moves
    private float speedMax = 30.0f; //how fast the animal moves

    private AnimalGameManager gameManager;
    private AnimalCollectionSpawnManager spawnManager;

    private bool hasTarget = false; //if the fish has a target to move towards
    private bool isTurning; //if the fish is turning

    private Vector3 targetPosition; // the position the fish is moving towards
    private Vector3 lastTargetPosition = new Vector3 (0f, 0f, 0f); // the last position the fish was moving towards

    // Start is called before the first frame update
    void Start()
    {
        //GameObject managerObject = GameObject.Find("AnimalCollectionSpawnManager"); //reference the AnimalGameManager
        //spawnManager = managerObject.GetComponent<AnimalCollectionSpawnManager>();
        spawnManager = transform.parent.GetComponent<AnimalCollectionSpawnManager>(); //reference the AnimalCollectionSpawnManager
        SetUpFish(); //set up the fish
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.up * Time.deltaTime * Random.Range(speedMin, speedMax)); //move the animal

        //if (transform.position.z > RightBoundry) //check if animal is out of bounds
        //{

        //    Destroy(gameObject);
        //}

        // If bool hasTarget is false...
        if (!hasTarget)
        {
            hasTarget = CanFindTarget(); //if the fish has a target to move towards
        }
        else
        {
            RotateFish(targetPosition, speed); //rotate the fish towards the target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); //move the fish towards the target position
        }

        if (transform.position == targetPosition)
        {
            hasTarget = false;
        }

    }

    void OnMouseDown() //when the player clicks an animal with mouse
    {
        if (gameObject.tag == "Invasive") //An 'invasive' animal is clicked
        {
            gameManager.Score += 1;
        }
        else //A 'native' animal is clicked
        {
            gameManager.Score -= 2;

            if (gameManager.Score < 0)
            {
                gameManager.Score = 0;
            }
        }

        Destroy(gameObject);
    }

    void SetUpFish()
    {
        float scale = Random.Range(0f, 3f);
        transform.localScale += new Vector3(scale * 1.5f, scale, scale); //set the size of the fish
    }

    bool CanFindTarget()
    {
        targetPosition = spawnManager.RandomFishWaypoint(); //get a random position for the fish to move towards

        if (lastTargetPosition == targetPosition)
        {
            targetPosition = spawnManager.RandomFishWaypoint(); //if the last target position is the same as the new target position, get a new target position
            return false; //return false if the fish has no target to move towards
        }
        else
        {
            lastTargetPosition = targetPosition; //set the last target position to the new target position
            speed = Random.Range(speedMin, speedMax); //set the speed of the fish
            return true; //return true if the fish has a target to move towards
        }
    }

    // Method to rotate the fish towards the target position
    void RotateFish(Vector3 waypoint, float currentSpeed)
    {
        float TurnSpeed = currentSpeed * Random.Range(1f, 3f); //set the turn speed of the fish
        Vector3 LookAt = waypoint - this.transform.position; //get the direction the fish is looking at
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(LookAt), TurnSpeed * Time.deltaTime); //rotate the fish towards the target position
    }
}
