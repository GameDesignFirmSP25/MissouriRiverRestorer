using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    // Method called when something enters the collider and triggers it
    void OnTriggerEnter(Collider other)
    {
        // If the gameObject has the tag "Trash: can"...
        if (other.gameObject.CompareTag("Trash: can"))
        {
            Debug.Log("Aluminum can entered trigger and was destroyed"); // Debug.Log
            Destroy(other.gameObject); // Destroy the object that triggered this collider
        }

        // If the gameObject has the tag "Trash: trash bag"...
        if (other.gameObject.CompareTag("Trash: trash bag"))
        {
            Debug.Log("Trash bag entered trigger and was destroyed"); // Debug.Log
            Destroy(other.gameObject); // Destroy the object that triggered this collider
        }

        // If the gameObject has the tag "Trash: tire"...
        if (other.gameObject.CompareTag("Trash: tire"))
        {
            Debug.Log("Tire entered trigger and was destroyed"); // Debug.Log
            Destroy(other.gameObject); // Destroy the object that triggered this collider
        }

        //// If the gameObject has the tag "Fish"...
        //if (other.gameObject.CompareTag("Fish"))
        //{
        //    Debug.Log("Fish entered trigger and was destroyed"); // Debug.Log
        //    Destroy(other.gameObject); // Destroy the object that triggered this collider
        //}

        //// If the gameObject has the tag "Test tube"...
        //if (other.gameObject.CompareTag("Test Tube"))
        //{
        //    Debug.Log("Test tube entered trigger and was destroyed"); // Debug.Log
        //    Destroy(other.gameObject); // Destroy the object that triggered this collider
        //}
    }
}
