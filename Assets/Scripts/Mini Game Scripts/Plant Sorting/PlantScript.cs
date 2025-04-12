using UnityEngine;

public class PlantScript : MonoBehaviour
{
    public float speed = 2f; //How fast the plant moves
    public float directionChangeInterval = 2f; // How often it changes direction
    private Vector3 direction; // The current movement direction
    private Rigidbody rb;
    private PlantGameManager plantGameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(ChooseNewDirection), directionChangeInterval, directionChangeInterval);  //Re-pick direction at regular intervals
        GameObject managerObject = GameObject.Find("PlantGameManager"); //reference the PlantGameManager
        plantGameManager = managerObject.GetComponent<PlantGameManager>();
        ChooseNewDirection(); //Start moving
    }

    void FixedUpdate()
    {
        Vector3 move = direction * speed * Time.fixedDeltaTime; // Move in current direction, only on XZ plane
        rb.MovePosition(rb.position + move);

        Vector3 pos = rb.position;
        rb.position = pos;
    }

    void ChooseNewDirection()
    { 
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized; // New random XZ direction
    }

    void OnCollisionEnter(Collision collision) // Bounce off walls
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 normal = contact.normal;
        direction = Vector3.Reflect(direction, normal);
    }

    void OnMouseDown() //When player clicks the plant check if not invasive then destroy
    {
        if (!gameObject.CompareTag("Invasive"))
        {
            plantGameManager.Score += 1;
        }
    Destroy(gameObject);
    }
}
