using UnityEngine;

public class FishAi : MonoBehaviour
{
    private float speed;
    private float minimumSpeed = 5f;
    private float maximumSpeed = 15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = Random.Range(minimumSpeed, maximumSpeed);
        // Move the object forward along its x axis 1 unit/second.
        transform.Translate(Vector3.right * Time.deltaTime * speed, Space.World);
    }
}
