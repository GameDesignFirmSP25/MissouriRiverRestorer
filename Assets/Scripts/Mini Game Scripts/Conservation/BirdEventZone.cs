using UnityEngine;
using UnityEngine.Events;

public class BirdEventZone : MonoBehaviour
{
    public static bool isBirdEventEntered = false; 
    public static bool birdEventTriggered = false; 

    public AnimalGameManager animalGameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isBirdEventEntered)
            {
                isBirdEventEntered = true;
                birdEventTriggered = true; 
                Debug.Log("Bird event triggered.");

                animalGameManager.BirdEventZoneEntered(); // Call the method in AnimalGameManager to handle the event
            }
            else
            {
                Debug.Log("Bird event already triggered.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isBirdEventEntered)
            {
                isBirdEventEntered = true;
                birdEventTriggered = true;
                Debug.Log("Bird event triggered.");
                animalGameManager.BirdEventZoneEntered(); // Call the method in AnimalGameManager to handle the event
            }
            else
            {
                Debug.Log("Bird event already triggered.");
            }
        }
    }
}
