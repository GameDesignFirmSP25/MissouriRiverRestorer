using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trash : MonoBehaviour
{
    private float speed;
    private float minimumSpeed = 12f;
    private float maximumSpeed = 25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    
    }

    private void Update()
    {
        speed = Random.Range(minimumSpeed, maximumSpeed);
        // Move the object forward along its x axis 1 unit/second.
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }


}
