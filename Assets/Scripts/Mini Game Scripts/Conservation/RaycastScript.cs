using UnityEngine;

public class RaycastScript : MonoBehaviour
{
    [Header("Camera")]
    public Camera _mainCamera;

    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Float Variables")]
    private float raycastDistance = 20f;

    [Header("Script References")]
    public AnimalGameManager animalGameManagerScript;

    [Header("Global Variables")]
    public static bool eventAnimalClicked = false;

    [Header("Player Reference")]
    public GameObject player; // Reference to the player GameObject

    [Header("Layers to Hit")]
    public LayerMask animalEventClicks;
    public LayerMask deerEvent;
    public LayerMask birdEvent;
    public LayerMask fishEvent;

    // Update is called once per frame
    void Update()
    {
        // Detect left mouse click.
        if (Input.GetMouseButtonDown(0))
        {
            // If no panel is active, proceed with raycasting
            CastRay(); // Call the CastRay function
        }
    }

    // Method to cast a ray from the camera to the mouse position
    void CastRay()
    {
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 2f);

        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, animalEventClicks))
        {
            Debug.Log($"Hit object: {hit.collider.gameObject.name}, Distance: {hit.distance}");
            Debug.Log($"deerEventActive: {animalGameManagerScript.deerEventActive}, birdEventActive: {animalGameManagerScript.birdEventActive}, fishEventActive: {animalGameManagerScript.fishEventActive}");

            if (animalGameManagerScript.deerEventActive)
            {
                Debug.Log("Deer event is active. Handling click on deer event animal.");
                eventAnimalClicked = true; // Set eventAnimalClicked to true
                HandleDeerEventClick(hit.collider.gameObject); // Handle the click on the deer GameObject in deer event
            }
            if (animalGameManagerScript.birdEventActive)
            {
                eventAnimalClicked = true; // Set eventAnimalClicked to true
                HandleBirdEventClick(hit.collider.gameObject); // Handle the click on the bird GameObject in bird event 
            }
            if (animalGameManagerScript.fishEventActive)
            {
                eventAnimalClicked = true; // Set eventAnimalClicked to true
                HandleFishEventClick(hit.collider.gameObject); // Handle the click on the asian carp GameObject in fish event
            }
        }
    }

    private void HandleDeerEventClick(GameObject clickedObject)
    {
        Debug.Log($"Deer clicked: {clickedObject.name}");

        // Get the ChangeNavAgentSpeed component from the clicked object
        ChangeNavAgentSpeed navAgentSpeed = clickedObject.GetComponent<ChangeNavAgentSpeed>();
        if (navAgentSpeed != null)
        {
            navAgentSpeed.SetAgentSpeed(3.5f); // Set the agent's speed to 3.5
        }
        else
        {
            Debug.LogWarning($"ChangeNavAgentSpeed component not found on {clickedObject.name}");
        }
    }

    private void HandleBirdEventClick(GameObject clickedObject)
    {
        Debug.Log($"Bird clicked: {clickedObject.name}");

        // Get the BirdFlight component from the clicked object
        BirdFlight birdFlight = clickedObject.GetComponent<BirdFlight>();
        if (birdFlight != null)
        {
            birdFlight.FlyAway(player.transform.position); // Trigger the bird to fly away
            Debug.Log($"Triggered FlyAway for {clickedObject.name}");
        }
        else
        {
            Debug.LogWarning($"BirdFlight component not found on {clickedObject.name}");
        }
    }

    private void HandleFishEventClick(GameObject clickedObject)
    {
        Debug.Log($"Fish clicked: {clickedObject.name}");
        Debug.Log($"Destroying fish: {clickedObject.name}");
        Destroy(clickedObject); // Destroy the fish GameObject
    }
}
