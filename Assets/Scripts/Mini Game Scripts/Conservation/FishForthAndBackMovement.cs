using UnityEngine;

public class FishForthAndBackMovement : MonoBehaviour
{
    [Header("Transforms")]
    public Transform pointA;
    public Transform pointB;

    [Header("Float Variables")]
    private float speed = 0.1f;
    private float rotationSpeed = 10f;

    [Header("Materials")]
    private Material eventInteraction;

    void Start()
    {
        Renderer renderer = GetComponentInChildren<Renderer>(); // Get the Renderer component of the interaction object 
        
        if (renderer != null)
        {
            eventInteraction = new Material(renderer.material); // Create unique instance
            eventInteraction.SetFloat("_OutlineType", 5);
            renderer.material = eventInteraction; // Assign one of them as the active material
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no Renderer component in children. Material setup skipped."); // Debug.LogWarning
        }
    }

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1f);
        // Determine the target position
        Vector3 targetPosition = Vector3.Lerp(pointB.position, pointA.position, time);

        // Calculate the direction to the target
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Rotate the fish to face the target direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        // Move the fish to the target position
        transform.position = targetPosition;

        if (AnimalGameManager.fishEventZoneComplete)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetOutline(bool enabled)
    {
        if (eventInteraction != null)
            eventInteraction.SetFloat("_HasOutline", enabled ? 1.0f : 0.0f);
    }
}
