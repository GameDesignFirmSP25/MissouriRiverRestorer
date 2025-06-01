using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LowerBankZoneTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI exploringText;

    public static bool lowerBankEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            lowerBankEntered = true;
            exploringText.text = "Lower Bank";
            //Debug.Log("Lower Bank Zone Triggered");
        }
    }
}
