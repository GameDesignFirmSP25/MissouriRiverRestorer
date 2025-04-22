using UnityEngine;
using UnityEngine.Events;

public class DeerEventZone : MonoBehaviour
{
    public bool isDeerEventActive = false;
    public static bool deerEventTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isDeerEventActive)
            {
                isDeerEventActive = true;
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

