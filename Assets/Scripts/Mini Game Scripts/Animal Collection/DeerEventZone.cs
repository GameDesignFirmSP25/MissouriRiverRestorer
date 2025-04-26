using UnityEngine;
using UnityEngine.Events;

public class DeerEventZone : MonoBehaviour
{
    public static bool isDeerEventEntered = false;
    public static bool deerEventTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isDeerEventEntered)
            {
                isDeerEventEntered = true;
                deerEventTriggered = true; 
                Debug.Log("Deer event triggered.");
            }
            else
            {
                Debug.Log("Deer event already triggered.");
            }
        }
    }
}

