using UnityEngine;

public class ColliderTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Destructible"))
        {
            Debug.Log("Object entered trigger and was destroyed");
            Destroy(other.gameObject); // Destroy the object that triggered this collider
        }    
    }
}
