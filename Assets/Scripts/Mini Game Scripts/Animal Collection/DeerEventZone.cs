using UnityEngine;
using UnityEngine.Events;

public class DeerEventZone : MonoBehaviour
{
    public bool isDeerEventActive = false; // Flag to check if the deer event is active
    public static bool deerEventTriggered = false; // Static flag to indicate if the deer event has been triggered
    //public UnityEvent onDeerEventStart; // Unity event to call when the deer event starts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isDeerEventActive)
            {
                isDeerEventActive = true;
                deerEventTriggered = true; // Set the static flag to true
                //onDeerEventStart.Invoke(); // Invoke the Unity event
                Debug.Log("Deer event triggered.");
            }
            else
            {
                Debug.Log("Deer event already triggered.");
            }
        }
    }
}

