using UnityEngine;
using UnityEngine.Events;

public class BirdEventZone : MonoBehaviour
{
    public bool isBirdEventActive = false; 
    public static bool birdEventTriggered = false; 
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isBirdEventActive)
            {
                isBirdEventActive = true;
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
