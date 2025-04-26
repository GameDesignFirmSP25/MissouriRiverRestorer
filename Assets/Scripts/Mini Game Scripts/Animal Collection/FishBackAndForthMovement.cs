using UnityEngine;

public class FishBackAndForthMovement : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    private float speed = 0.1f;
    private float rotationSpeed = 10f;
    private bool goingToA = true;
    //public AnimalGameManager animalGameManager;

    void Update()
    {
        float time = Mathf.PingPong(Time.time * speed, 1f);
        // Determine the target position
        Vector3 targetPosition = Vector3.Lerp(pointA.position, pointB.position, time);

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
}
