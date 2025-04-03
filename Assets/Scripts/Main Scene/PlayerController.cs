using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of player movement
    public float rotationSpeed = 100f; // Speed of rotation

    private void Start()
    {
          
    }
    void Update()
    {
        // Get input from the horizontal and vertical axes for movement
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
       
        
        
        //may try to get this script functional to have the player camera rotate
        //// Get input from the mouse movement axes for rotation
        //float mouseX = Input.GetAxis("Mouse X"); // Horizontal mouse movement
        //float mouseY = Input.GetAxis("Mouse Y"); // Vertical mouse movement (optional)

        //// Rotate the player based on the mouse X axis
        //transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        //// Optional: Add rotation based on Mouse Y axis if needed
        //// Example: Tilt the player up/down (can cause unintended effects in some cases)
        //// transform.Rotate(-mouseY * rotationSpeed * Time.deltaTime, 0, 0);
    }
}

