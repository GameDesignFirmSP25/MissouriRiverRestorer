using UnityEngine;
using UnityEngine.Events;

public class BirdEventZone : MonoBehaviour
{
    public static bool isBirdEventEntered = false; 
    public static bool birdEventTriggered = false; 
    

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
            }
            else
            {
                Debug.Log("Bird event already triggered.");
            }
        }
    }
}
