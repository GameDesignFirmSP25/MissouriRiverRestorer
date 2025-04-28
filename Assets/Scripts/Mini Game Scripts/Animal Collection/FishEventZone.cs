using UnityEngine;
using UnityEngine.Events;

public class FishEventZone : MonoBehaviour
{
    public static bool isFishEventEntered = false; 
    public static bool fishEventTriggered = false;

    public AnimalGameManager animalGameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isFishEventEntered)
            {
                isFishEventEntered = true;
                fishEventTriggered = true;
                Debug.Log("Fish event triggered.");

                animalGameManager.FishEventZoneEntered();
            }
            else
            {
                Debug.Log("Fish event already triggered.");
            }
        }
    }
}
