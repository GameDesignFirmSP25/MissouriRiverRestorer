using UnityEngine;

public class SkyTriggerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Object entered the trigger collider: {other.gameObject.name}. Dectroying...");
        Destroy(other.gameObject); // Destroy the object that enters the trigger collider
    }
}
