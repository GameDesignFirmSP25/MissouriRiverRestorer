using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UpperBankZoneTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI exploringText;

    public static bool upperBankEntered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            upperBankEntered = true;
            exploringText.text = "Upper Bank";
            //Debug.Log("Upper Bank Zone Triggered");
        }
    }
}
