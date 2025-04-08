using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  // Speed of player movement
     private CharacterController controller;
     private float gravity;

     private void Awake()
     {
          controller = gameObject.GetComponent<CharacterController>();
     }

     void Update()
    {
        // Get input from the horizontal and vertical axes for movement
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down arrows

          // Create a movement vector
          gravity -= 9.81f * Time.deltaTime;
          if (controller.isGrounded) gravity = 0;
          Vector3 move = new Vector3(moveX, gravity, moveZ);

          // Normalize the movement vector to maintain consistent speed diagonally
          if (move.magnitude > 1) move.Normalize();

        // Move the player
          if(move != Vector3.zero)
          {
               controller.Move(move * moveSpeed * Time.deltaTime);

               Vector3 lookRotVector = new Vector3(move.x, 0, move.z);
               if(lookRotVector != Vector3.zero)
               {
                    transform.rotation = Quaternion.LookRotation(new Vector3(move.x, 0, move.z));
               }
          }
     }
}

