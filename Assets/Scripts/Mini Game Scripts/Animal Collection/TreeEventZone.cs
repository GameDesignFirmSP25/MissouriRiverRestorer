using UnityEngine;

public class TreeEventZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Start event
            Debug.Log("Zone triggered. Start Eastern Starling event.");
        }
    }
}
