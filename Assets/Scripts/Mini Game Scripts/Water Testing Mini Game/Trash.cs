using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trash : MonoBehaviour
{
    private float speed;
    private float minimumSpeed = 2f;
    private float maximumSpeed = 8f;

    [SerializeField]
    private float angle = 45f; // Angle in degrees for initial movement

    [SerializeField]
    private float switchPointX = 55f; // X-coordinate at which the trash moves straight

    private Vector3 angledDirection;

    private void Start()
    {
        // Calculate the angled direction based on the angle
        float radians = angle * Mathf.Deg2Rad;
        angledDirection = new Vector3(Mathf.Sin(radians), 0, Mathf.Cos(radians)).normalized;
    }

    private void Update()
    {
        speed = Random.Range(minimumSpeed, maximumSpeed); // Randomize speed

        // Check if the trash has reached the switch point
        if (transform.position.x < switchPointX)
        {
            // Move at an angle
            transform.Translate(angledDirection * Time.deltaTime * speed, Space.World);
        }
        else
        {
            // Move straight down
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World);
        }
    }

    //private void Update()
    //{
    //    speed = Random.Range(minimumSpeed, maximumSpeed); // speed equals random number within range of minimum speed and maximum speed
    //    transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.World); // Move the object forward along its x axis 1 unit/second.
    //}


}
