using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Reference to the player's Transform
    public Vector3 offset;    // Offset for the camera's position (e.g., distance behind the player)
     public float rotationSpeed = 100f; // Speed of rotation

     void LateUpdate()
    {
          // Update the camera's position
          transform.position = target.TransformPoint(offset);

          if (Input.GetKey(KeyCode.Q))
          {
               target.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
          }
          if (Input.GetKey(KeyCode.E))
          {
               target.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
          }

          // Make the camera look at the player
          transform.LookAt(target);
    }
}
