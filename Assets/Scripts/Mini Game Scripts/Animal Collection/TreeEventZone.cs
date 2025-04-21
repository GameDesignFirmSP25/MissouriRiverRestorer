using UnityEngine;
using UnityEngine.Events;

public class TreeEventZone : MonoBehaviour
{
    public bool isTreeEventActive = false; // Flag to check if the tree event is active
    public static bool treeEventTriggered = false; // Static flag to indicate if the tree event has been triggered
    //public UnityEvent onTreeEventStart; // Unity event to call when the tree event starts

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            if (!isTreeEventActive)
            {
                isTreeEventActive = true;
                treeEventTriggered = true; // Set the static flag to true
                //onTreeEventStart.Invoke(); // Invoke the Unity event
                Debug.Log("Tree event triggered.");
            }
            else
            {
                Debug.Log("Tree event already triggered.");
            }
        }
    }
}
