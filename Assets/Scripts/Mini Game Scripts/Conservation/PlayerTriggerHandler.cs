using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerHandler : MonoBehaviour
{
    [Header("Booleans")]
    public bool lowerBankEntered = false;
    public bool midBankEntered = false;
    public bool upperBankEntered = false;

    // Method called when the player enters a trigger collider
    private void OnTriggerEnter(Collider other)
    {
        // If the player enters a trigger collider with the tag "Zone: Lower Bank"...
        if (other.CompareTag("Zone: Lower Bank"))
        {
            lowerBankEntered = true; // Set bool lowerBankEntered to true
        }

        // If the player enters a trigger collider with the tag "Zone: Mid Bank"...
        if (other.CompareTag("Zone: Mid Bank"))
        {
            midBankEntered = true;
        }

        // If the player enters a trigger collider with the tag "Zone: River"...
        if (other.CompareTag("Zone: Upper Bank"))
        {
            upperBankEntered = true;
        }
    }
}
