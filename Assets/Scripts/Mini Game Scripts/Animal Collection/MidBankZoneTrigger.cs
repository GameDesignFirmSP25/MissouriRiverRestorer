using UnityEngine;
using UnityEngine.Events;

public class MidBankZoneTrigger : MonoBehaviour
{
    public static bool midBankEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            midBankEntered = true;
        }
    }
}
