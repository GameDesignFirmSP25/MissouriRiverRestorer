using UnityEngine;

public class CameraFollowFixedHeight : MonoBehaviour
{
    public Transform player;  // Reference to the player's Transform
    public Vector3 offset;    // Offset for the camera's position (e.g., distance behind the player)

    void LateUpdate()
    {
        // Keep the camera at the same height while following the player
        Vector3 newPosition = new Vector3(player.position.x + offset.x, transform.position.y, player.position.z + offset.z);

        // Update the camera's position
        transform.position = newPosition;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}
