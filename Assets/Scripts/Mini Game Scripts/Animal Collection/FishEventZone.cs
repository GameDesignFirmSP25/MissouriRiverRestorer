using UnityEngine;
using UnityEngine.Events;

public class FishEventZone : MonoBehaviour
{
    public bool isFishEventActive = false; // Flag to check if the fish event is active
    public static bool fishEventTriggered = false; // Static flag to indicate if the fish event has been triggered
    //public UnityEvent onFishEventStart; // Unity event to call when the fish event starts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isFishEventActive)
            {
                isFishEventActive = true;
                fishEventTriggered = true; // Set the static flag to true
                //onFishEventStart.Invoke(); // Invoke the Unity event
                Debug.Log("Fish event triggered.");
            }
            else
            {
                Debug.Log("Fish event already triggered.");
            }
        }
    }
}
