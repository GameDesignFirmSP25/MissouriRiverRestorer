using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's Transform
    public Vector3 offset;    // Offset for the camera's position (e.g., distance behind the player)
     public float rotationSpeed = 100f; // Speed of rotation

     void LateUpdate()
    {
        // Update the camera's position
        transform.localPosition = offset;

        // Make the camera look at the player
        transform.LookAt(target);
    }
}
