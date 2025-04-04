using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
     public float rotationSpeed = 100.0f; // Speed of camera rotation
     public Transform target;
     public float angleClamp = 30;

     void LateUpdate()
     {

          transform.position = target.position;
          float mouseX = Input.GetAxis("Mouse X");

          // Rotate the target based on the mouse X axis
          transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

          // Enable this for angle clamping. If we decide we want a fixed view
/*          if(transform.rotation.eulerAngles.y < 180 && transform.rotation.eulerAngles.y > angleClamp)
          {
               transform.rotation = Quaternion.Euler(Vector3.up * angleClamp);
          }
          else if (transform.rotation.eulerAngles.y > 180 && transform.rotation.eulerAngles.y < 360 - angleClamp)
          {
               transform.rotation = Quaternion.Euler(Vector3.up * (360 -angleClamp));
          }*/
     }
}