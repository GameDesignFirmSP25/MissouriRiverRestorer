using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    // Method called when something enters the collider and triggers it
    void OnTriggerEnter(Collider other)
    {
        // If the gameObject has the tag "destructible"...
        if (other.gameObject.CompareTag("Destructible"))
        {
            Debug.Log("Object entered trigger and was destroyed"); // Debug.Log "Oject entered trigger and was destroyed"
            Destroy(other.gameObject); // Destroy the object that triggered this collider
        }    
    }
}
