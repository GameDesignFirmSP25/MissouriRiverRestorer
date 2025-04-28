using UnityEngine;
using UnityEngine.Events;

public class UpperBankZoneTrigger : MonoBehaviour
{
    public static bool upperBankEntered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upperBankEntered = true;
            Debug.Log("Upper Bank Zone Triggered");
        }
    }
}
