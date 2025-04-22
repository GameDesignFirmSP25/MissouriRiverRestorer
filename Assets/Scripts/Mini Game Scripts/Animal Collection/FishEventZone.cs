using UnityEngine;
using UnityEngine.Events;

public class FishEventZone : MonoBehaviour
{
    public bool isFishEventActive = false; 
    public static bool fishEventTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isFishEventActive)
            {
                isFishEventActive = true;
                fishEventTriggered = true;
                Debug.Log("Fish event triggered.");
            }
            else
            {
                Debug.Log("Fish event already triggered.");
            }
        }
    }
}
