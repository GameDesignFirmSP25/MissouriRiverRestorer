using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trash : MonoBehaviour
{
    private float speed;
    private float minimumSpeed = 10f;
    private float maximumSpeed = 25f;

    private void Update()
    {
        speed = Random.Range(minimumSpeed, maximumSpeed); // speed equals random number within range of minimum speed and maximum speed
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World); // Move the object forward along its x axis 1 unit/second.
    }


}
