using UnityEngine;
using UnityEngine.Events;

public class LowerBankZoneTrigger : MonoBehaviour
{
    public static bool lowerBankEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lowerBankEntered = true;
        }
    }
}
