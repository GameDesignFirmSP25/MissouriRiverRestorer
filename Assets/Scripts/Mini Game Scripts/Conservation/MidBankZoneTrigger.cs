using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MidBankZoneTrigger : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI exploringText;

    public static bool midBankEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            midBankEntered = true;
            exploringText.text = "Mid Bank";
            //Debug.Log("Mid Bank Zone Triggered");
        }
    }
}
