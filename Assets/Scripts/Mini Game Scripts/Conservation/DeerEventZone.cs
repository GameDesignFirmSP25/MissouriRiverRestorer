using UnityEngine;
using UnityEngine.Events;

public class DeerEventZone : MonoBehaviour
{
    public static bool isDeerEventEntered = false;
    public static bool deerEventTriggered = false;

    public AnimalGameManager animalGameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isDeerEventEntered)
            {
                isDeerEventEntered = true;
                deerEventTriggered = true; 
                //Debug.Log("Deer event triggered.");

                animalGameManager.DeerEventZoneEntered(); // Call the method in AnimalGameManager to handle the event

            }
            else
            {
                //Debug.Log("Deer event already triggered.");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isDeerEventEntered)
            {
                isDeerEventEntered = true;
                deerEventTriggered = true;
                //Debug.Log("Deer event triggered.");

                animalGameManager.DeerEventZoneEntered(); // Call the method in AnimalGameManager to handle the event
            }
            else
            {
                //Debug.Log("Deer event already triggered.");
            }
        }
    }
}

