using UnityEngine;

public class CircularAi : MonoBehaviour
{
    public float speed = 2f;
    public float radius = 5f;
    public Transform pivotPoint;
    public Vector3 center;
    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // If a pivot point is assigned, use its position as the center
        if (pivotPoint != null)
        {
            center = pivotPoint.position;
        }
        else
        {
            center = transform.position; // Default to the object's starting position
        }
    }
    void Update()
    {
        // Update the angle
        angle -= speed * Time.deltaTime;

        // Calculate the target position
        float x = center.x + radius * Mathf.Cos(angle);
        float z = center.z + radius * Mathf.Sin(angle);
        Vector3 targetPosition = new Vector3(x, transform.position.y, z);

        // Calculate the direction to the target position
        Vector3 direction = targetPosition - transform.position;

        // Rotate the fish to face the direction of movement
        if (direction != Vector3.zero) // Avoid errors when direction is zero
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        // Move the fish to the target position
        transform.position = targetPosition;
    }
}
