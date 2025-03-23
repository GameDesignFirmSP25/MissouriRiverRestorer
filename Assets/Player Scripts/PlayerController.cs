using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of player movement

    void Update()
    {
        // Get input from the horizontal and vertical axes
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down arrows

        // Create a movement vector
        Vector3 move = new Vector3(moveX, 0, moveZ);

        // Normalize the movement vector to maintain consistent speed diagonally
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        // Move the player
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}

