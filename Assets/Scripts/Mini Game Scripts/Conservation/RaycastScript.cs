using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class RaycastScript : MonoBehaviour
{
    [Header("Target Objects")]
    [SerializeField]
    private string[] easternStarlingNames;

    [SerializeField]
    private string[] whiteTailedDeerNames;

    [SerializeField]
    private string[] bandedPennantDragonflyNames;

    [SerializeField]
    private string[] garterSnakeNames;

    [SerializeField]
    private string[] baldEagleNames;

    [SerializeField]
    private string[] paintedLadyButterflyNames;

    [SerializeField]
    private string[] asianCarpNames;

    [SerializeField]
    private string[] beaverNames;

    [SerializeField]
    private string[] raccoonNames;

    [SerializeField]
    private string[] muskratNames;

    [SerializeField]
    private string[] snappingTurtleNames;

    [SerializeField]
    private string[] northernMapTurtleNames;

    //[SerializeField]
    //private string[] bradfordPearTreeNames;

    [Header("Camera")]
    public Camera _mainCamera;

    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Float Variables")]
    private float raycastDistance = 20f; 

    [Header("Script References")]
    public AnimalGameManager animalGameManagerScript;
    public ChangeablePlant changeablePlantScript;
    public ObjectManager objectManagerScript;

    //[Header("Booleans")]
    //public static bool easternStarlingClicked = false;
    //public static bool whiteTailedDeerClicked = false;
    //public static bool bandedPennantDragonflyClicked = false;
    //public static bool garterSnakeClicked = false;
    //public static bool baldEagleClicked = false;
    //public static bool paintedLadyButterflyClicked = false;
    //public static bool asianCarpClicked = false;
    //public static bool beaverClicked = false;
    //public static bool raccoonClicked = false;
    //public static bool muskratClicked = false;
    //public static bool snappingTurtleClicked = false;
    //public static bool northernMapTurtleClicked = false;
    ////public static bool bradfordPearTreeClicked = false;
    ////public static bool purpleLoosestrifeClicked = false;
    //public static bool wasEasternStarlingPreviouslyClicked = false;
    //public static bool wasWhiteTailedDeerPreviouslyClicked = false;
    //public static bool wasBandedPennantDragonflyPreviouslyClicked = false;
    //public static bool wasGarterSnakePreviouslyClicked = false;
    //public static bool wasBaldEaglePreviouslyClicked = false;
    //public static bool wasPaintedLadyButterflyPreviouslyClicked = false;
    //public static bool wasAsianCarpPreviouslyClicked = false;
    //public static bool wasBeaverPreviouslyClicked = false;
    //public static bool wasRaccoonPreviouslyClicked = false;
    //public static bool wasMuskratPreviouslyClicked = false;
    //public static bool wasSnappingTurtlePreviouslyClicked = false;
    //public static bool wasNorthernMapTurtlePreviouslyClicked = false;
    //public static bool wasBradfordPearTreePreviouslyClicked = false;
    public static bool eventAnimalClicked = false;

    [Header("Player Reference")]
    public GameObject player; // Reference to the player GameObject

    [Header("Layers to Hit")]
    public LayerMask clickable;
    public LayerMask animalEventClicks;
    public LayerMask deerEvent;
    public LayerMask birdEvent;
    public LayerMask fishEvent;
    public LayerMask bradfordPearTreeInteraction;
    public LayerMask purpleLoosestrifeInteraction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set list of eastern starling names
        easternStarlingNames = new string[]
        {
            "European (Eastern) Starling", "European (Eastern) Starling (1)", "European (Eastern) Starling (2)",
            "European (Eastern) Starling (3)"
        };

        // Set list of white-tailed deer names
        whiteTailedDeerNames = new string[]
        {
            "White-Tailed Deer", "White-Tailed Deer (1)", "White-Tailed Deer (2)",
            "White-Tailed Deer (3)", "White-Tailed Deer (4)", "White-Tailed Deer (Buck)",
            "White-Tailed Deer (Buck) (1)", "White-Tailed Deer (Buck) (2)", "White-Tailed Deer (Buck) (3)",
            "White-Tailed Deer (Buck) (4)"
        };

        // Set list of banded pennant dragonfly names
        bandedPennantDragonflyNames = new string[]
        {
            "Banded Pennant Dragonfly", "Banded Pennant Dragonfly (1)", "Banded Pennant Dragonfly (2)",
            "Banded Pennant Dragonfly (3)", "Banded Pennant Dragonfly (4)"
        };

        // Set list of garter snake names
        garterSnakeNames = new string[]
        {
            "Garter Snake", "Garter Snake (1)", "Garter Snake (2)",
            "Garter Snake (3)", "Garter Snake (4)", "Garter Snake (5)"
        };

        // Set list of bald eagle names
        baldEagleNames = new string[]
        {
            "Bald Eagle", "Bald Eagle (1)"
        };

        // Set list of painted lady butterfly names
        paintedLadyButterflyNames = new string[]
        {
            "Painted Lady Butterfly", "Painted Lady Butterfly (1)", "Painted Lady Butterfly (2)",
            "Painted Lady Butterfly (3)"
        };

        // Set list of asian carp names
        asianCarpNames = new string[]
        {
            "Asian Carp", "Asian Carp (1)", "Asian Carp (2)",
            "Asian Carp (3)", "Asian Carp (4)", "Asian Carp (5)",
            "Asian Carp (6)", "Asian Carp (7)", "Asian Carp (8)"
        };

        // Set list of beaver names
        beaverNames = new string[]
        {
            "Beaver", "Beaver (1)", "Beaver (2)",
            "Beaver (3)"
        };

        // Set list of raccoon names
        raccoonNames = new string[]
        {
            "Raccoon", "Raccoon (1)", "Raccoon (2)",
            "Raccoon (3)", "Raccoon (4)", "Raccoon (5)"
        };

        // Set list of muskrat names
        muskratNames = new string[]
        {
            "Muskrat", "Muskrat (1)", "Muskrat (2)",
        };

        // Set list of snapping turtle names
        snappingTurtleNames = new string[]
        {
            "Snapping Turtle", "Snapping Turtle (1)", "Snapping Turtle (2)"
        };

        // Set list of northern map turtle names
        northernMapTurtleNames = new string[]
        {
            "Northern Map Turtle", "Northern Map Turtle (1)", "Northern Map Turtle (2)",
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Detect left mouse click.
        if (Input.GetMouseButtonDown(0))
        {
            //// Check if a panel is active
            //if (AnimalGameManager.dialogueIsActive || AnimalGameManager.eventZonePanelActive)
            //{
            //    Debug.Log("A panel is active. Restricting clicks to the panel.");
            //    return; // Prevent clicks on other objects
            //}

            //// If deer event is active, restrict clicks to event animals
            //if (animalGameManagerScript.deerEventActive && !eventAnimalClicked)
            //{
            //    Debug.Log("Event is active. Restricting clicks to event animals.");
            //    return; // Prevent clicks on other objects
            //}

            //// If bird event is active, restrict clicks to event animals
            //if (animalGameManagerScript.birdEventActive && !eventAnimalClicked)
            //{
            //    Debug.Log("Event is active. Restricting clicks to event animals.");
            //    return; // Prevent clicks on other objects
            //}

            //// If fish event is active, restrict clicks to event animals
            //if (animalGameManagerScript.fishEventActive && !eventAnimalClicked)
            //{
            //    Debug.Log("Event is active. Restricting clicks to event animals.");
            //    return; // Prevent clicks on other objects
            //}

            // If no panel is active, proceed with raycasting
            CastRay(); // Call the CastRay function
        }
    }

    // Method to cast a ray from the camera to the mouse position
    void CastRay()
    {
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 2f);

        //if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, clickable)) // Perform the raycast
        //{
        //    if (hit.distance <= raycastDistance)
        //    {
        //        Debug.Log($"Hit object: {hit.collider.gameObject.name}, Distance: {hit.distance}");

        //        //if (animalGameManagerScript.lowerBankObjectivesActive)
        //        //{
        //        //    if (System.Array.Exists(snappingTurtleNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleSnappingTurtleClick(hit.collider.gameObject); // Handle the click on the snapping turtle GameObject 
        //        //    }
        //        //    else if (System.Array.Exists(garterSnakeNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleGarterSnakeClick(hit.collider.gameObject); // Handle the click on the garter snake GameObject
        //        //    }
        //        //    else if (System.Array.Exists(northernMapTurtleNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleNorthernMapTurtleClick(hit.collider.gameObject); // Handle the click on the northern map turtle GameObject 
        //        //    }
        //        //    else if (System.Array.Exists(asianCarpNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleAsianCarpClick(hit.collider.gameObject); // Handle the click on the asian carp GameObject 
        //        //    }
        //        //    else if (System.Array.Exists(beaverNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleBeaverClick(hit.collider.gameObject); // Handle the click on the beaver GameObject 
        //        //    }
        //        //}

        //        //if (animalGameManagerScript.midBankObjectivesActive)
        //        //{
        //        //    // If the clicked GameObject's name matches the target name for eastern starling...
        //        //    if (System.Array.Exists(easternStarlingNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleEasternStarlingClick(hit.collider.gameObject); // Handle the click on the eastern starling GameObject
        //        //    }
        //        //    // Check if the clicked GameObject's name matches any banded pennant dragonfly name
        //        //    else if (System.Array.Exists(bandedPennantDragonflyNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleBandedPennantDragonflyClick(hit.collider.gameObject); // Handle the click on the banded pennant dragonfly GameObject
        //        //    }
        //        //    else if (System.Array.Exists(muskratNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleMuskratClick(hit.collider.gameObject); // Handle the click on the muskrat GameObject 
        //        //    }
        //        //}
                
        //        //if (animalGameManagerScript.upperBankObjectivesActive)
        //        //{
        //        //    // Check if the clicked GameObject's name matches any white-tailed deer name
        //        //    if (System.Array.Exists(whiteTailedDeerNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleWhiteTailedDeerClick(hit.collider.gameObject); // Handle the click on the white-tailed deer GameObject
        //        //    }
        //        //    else if (System.Array.Exists(paintedLadyButterflyNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandlePaintedLadyButterflyClick(hit.collider.gameObject); // Handle the click on the painted lady butterfly GameObject 
        //        //    }
        //        //    else if (System.Array.Exists(raccoonNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleRaccoonClick(hit.collider.gameObject); // Handle the click on the raccoon GameObject 
        //        //    }
        //        //    else if (System.Array.Exists(baldEagleNames, name => name == hit.collider.gameObject.name))
        //        //    {
        //        //        HandleBaldEagleClick(hit.collider.gameObject); // Handle the click on the bald eagle GameObject 
        //        //    }
        //        //} 
        //    }
        //    else
        //    {
        //        Debug.Log($"Ignored object: {hit.collider.gameObject.name}, Distance: {hit.distance}");
        //    }
        //}

        //if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, animalEventClicks)) 
        //{
        //    Debug.Log($"Hit object: {hit.collider.gameObject.name}, Distance: {hit.distance}");

        //    EventAnimalClicked();
        //}

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

        //if (animalGameManagerScript.eventZonesComplete)
        //{
        //    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, bradfordPearTreeInteraction))
        //    {
        //        Debug.Log("Click on Bradford Pear Tree detected.");

        //        if (hit.distance <= raycastDistance)
        //        {
        //            Debug.Log($"Hit object: {hit.collider.gameObject.name}, Distance: {hit.distance}");

        //            if (changeablePlantScript == null)
        //            {
        //                Debug.LogError("ChangeablePlantScript is not assigned!");
        //                return;
        //            }

        //            if (hit.collider == null)
        //            {
        //                Debug.LogWarning("No collider was hit by the raycast.");
        //                return;
        //            }

        //            if (!changeablePlantScript.isSwapped)
        //            {
        //                Debug.Log($"Swapping plant: {hit.collider.gameObject.name}");
        //                //changeablePlantScript.SwapPlants(hit.collider.gameObject);

        //                HandleBradfordPearTreeClick(hit.collider.gameObject); // Handle the click on the bradford pear tree GameObject
        //            }
        //            else
        //            {
        //                Debug.Log("Plant has already been swapped.");
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log($"Ignored object: {hit.collider.gameObject.name}, Distance: {hit.distance}");
        //        }
        //    }

        //    if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, purpleLoosestrifeInteraction))
        //    {
        //        Debug.Log("Click on Purple Loosestrife detected.");

        //        if (hit.distance <= raycastDistance)
        //        {
        //            Debug.Log($"Hit object: {hit.collider.gameObject.name}, Distance: {hit.distance}");

        //            if (changeablePlantScript == null)
        //            {
        //                Debug.LogError("ChangeablePlantScript is not assigned!");
        //                return;
        //            }

        //            if (hit.collider == null)
        //            {
        //                Debug.LogWarning("No collider was hit by the raycast.");
        //                return;
        //            }

        //            if (!changeablePlantScript.isSwapped)
        //            {
        //                Debug.Log($"Swapping plant: {hit.collider.gameObject.name}");
        //                //changeablePlantScript.SwapPlants(hit.collider.gameObject);

        //                HandlePurpleLoosestrifeClick(hit.collider.gameObject); // Handle the click on the bradford pear tree GameObject
        //            }
        //            else
        //            {
        //                Debug.Log("Plant has already been swapped.");
        //            }
        //        }
        //        else
        //        {
        //            Debug.Log($"Ignored object: {hit.collider.gameObject.name}, Distance: {hit.distance}");
        //        }
        //    }
        //} 
    }

    //// Handle clicks on eastern starling
    //private void HandleEasternStarlingClick(GameObject clickedObject)
    //{
    //    // If bool easternStarlingClicked is false...
    //    if (!easternStarlingClicked && !wasEasternStarlingPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        easternStarlingClicked = true; // Set bool easternStarlingClicked to true
    //        wasEasternStarlingPreviouslyClicked = true; // Set bool wasEasternStarlingPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("European Starling"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Eastern Starling has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handle clicks on white-tailed deer
    //private void HandleWhiteTailedDeerClick(GameObject clickedObject)
    //{
    //    // If bool whiteTailedDeerClicked is false...
    //    if (!whiteTailedDeerClicked && !wasWhiteTailedDeerPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        whiteTailedDeerClicked = true; // Set bool whiteTailedDeerClicked to true
    //        wasWhiteTailedDeerPreviouslyClicked = true; // Set bool wasWhiteTailedDeerPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("White-Tailed Deer"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("White-tailed Deer has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on banded pennant dragonfly
    //private void HandleBandedPennantDragonflyClick(GameObject clickedObject)
    //{
    //    // If bool bandedPennantDragonflyClicked is false...
    //    if (!bandedPennantDragonflyClicked && !wasBandedPennantDragonflyPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        bandedPennantDragonflyClicked = true; // Set bool bandedPennantDragonflyClicked to true
    //        wasBandedPennantDragonflyPreviouslyClicked = true; // Set bool wasBandedPennantDragonflyPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Banded Pennant Dragonfly"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Banded Pennant Dragonfly has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on garter snake
    //private void HandleGarterSnakeClick(GameObject clickedObject)
    //{
    //    // If bool garterSnakeClicked is false...
    //    if (!garterSnakeClicked && !wasGarterSnakePreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        garterSnakeClicked = true; // Set bool garterSnakeClicked to true
    //        wasGarterSnakePreviouslyClicked = true; // Set bool wasGarterSnakePreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Common Garter Snake"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Garter Snake has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on bald eagle
    //private void HandleBaldEagleClick(GameObject clickedObject)
    //{
    //    // If bool baldEagleClicked is false...
    //    if (!baldEagleClicked && !wasBaldEaglePreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        baldEagleClicked = true; // Set bool baldEagleClicked to true
    //        wasBaldEaglePreviouslyClicked = true; // Set bool wasBaldEaglePreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Bald Eagle"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Bald Eagle has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on bald eagle
    //private void HandlePaintedLadyButterflyClick(GameObject clickedObject)
    //{
    //    // If bool baldEagleClicked is false...
    //    if (!paintedLadyButterflyClicked && !wasPaintedLadyButterflyPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        paintedLadyButterflyClicked = true; // Set bool baldEagleClicked to true
    //        wasPaintedLadyButterflyPreviouslyClicked = true; // Set bool wasPaintedLadyButterflyPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Painted Lady Butterfly"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Painted Lady Butterfly has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on asian carp
    //private void HandleAsianCarpClick(GameObject clickedObject)
    //{
    //    // If bool asianCarpClicked is false...
    //    if (!asianCarpClicked && !wasAsianCarpPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        asianCarpClicked = true; // Set bool asianCarpClicked to true
    //        wasAsianCarpPreviouslyClicked = true; // Set bool wasAsianCarpPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Asian Carp"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Asian Carp has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on beaver
    //private void HandleBeaverClick(GameObject clickedObject)
    //{
    //    // If bool beaverClicked is false...
    //    if (!beaverClicked && !wasBeaverPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        beaverClicked = true; // Set bool beaverClicked to true
    //        wasBeaverPreviouslyClicked = true; // Set bool wasBeaverPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Beaver"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Beaver has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on raccoon
    //private void HandleRaccoonClick(GameObject clickedObject)
    //{
    //    // If bool raccoonClicked is false...
    //    if (!raccoonClicked && !wasRaccoonPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        raccoonClicked = true; // Set bool raccoonClicked to true
    //        wasRaccoonPreviouslyClicked = true; // Set bool wasRaccoonPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Raccoon"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Raccoon has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on muskrat
    //private void HandleMuskratClick(GameObject clickedObject)
    //{
    //    // If bool muskeratClicked is false...
    //    if (!muskratClicked && !wasMuskratPreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        muskratClicked = true; // Set bool muskeratClicked to true
    //        wasMuskratPreviouslyClicked = true; // Set bool wasMuskratPreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Muskrat"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Muskrat has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on snapping turtle
    //private void HandleSnappingTurtleClick(GameObject clickedObject)
    //{
    //    // If bool snappingTurtleClicked is false...
    //    if (!snappingTurtleClicked && !wasSnappingTurtlePreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        snappingTurtleClicked = true; // Set bool snappingTurtleClicked to true
    //        wasSnappingTurtlePreviouslyClicked = true; // Set bool wasSnappingTurtlePreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Snapping Turtle"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Snapping Turtle has already been clicked."); // Debug.Log
    //    }
    //}

    //// Handles clicks on northern map turtle
    //private void HandleNorthernMapTurtleClick(GameObject clickedObject)
    //{
    //    // If bool northernMapTurtleClicked is false...
    //    if (!northernMapTurtleClicked && !wasNorthernMapTurtlePreviouslyClicked)
    //    {
    //        Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
    //        northernMapTurtleClicked = true; // Set bool northernMapTurtleClicked to true
    //        wasNorthernMapTurtlePreviouslyClicked = true; // Set bool wasNorthernMapTurtlePreviouslyClicked to true
    //        animalGameManagerScript.AnimalsFound += 1;
    //        //animalGameManagerScript.UpdateAnimalCounter();

    //        ObjectManager.instance.SetObjectFound("Northern Map Turtle"); // Set the object as found in the ObjectManager
    //    }
    //    else
    //    {
    //        Debug.Log("Northern Map Turtle has already been clicked."); // Debug.Log
    //    }
    //}

    // Handles click on bradford pear tree
    private void HandleBradfordPearTreeClick(GameObject clickedObject)
    {
        // Get the ChangeablePlant component from the clicked object
        ChangeablePlant clickedPlant = clickedObject.GetComponent<ChangeablePlant>();
        if (clickedPlant != null)
        {
            Debug.Log($"Bradford Pear Tree clicked! Plant ID: {clickedPlant.plantID}");
            //bradfordPearTreeClicked = true; // Set the boolean to true
            //                                // Pass the clicked plant to AnimalGameManager
            animalGameManagerScript.changeablePlant = clickedPlant; // Assign the clicked plant to the AnimalGameManager script
            animalGameManagerScript.BradfordPearTreeClicked(clickedPlant);

            ObjectManager.instance.SetObjectFound("Bradford Pear"); // Set the object as found in the ObjectManager
        }
        else
        {
            Debug.LogWarning("Clicked object does not have a ChangeablePlant component.");
        }
    }

    // Handles click on purple loosestrife
    private void HandlePurpleLoosestrifeClick(GameObject clickedObject)
    {
        // Get the ChangeablePlant component from the clicked object
        ChangeablePlant clickedPlant = clickedObject.GetComponent<ChangeablePlant>();
        if (clickedPlant != null)
        {
            Debug.Log($"Purple Loosestrife clicked! Plant ID: {clickedPlant.plantID}");
            //purpleLoosestrifeClicked = true; // Set the boolean to true
            //                                // Pass the clicked plant to AnimalGameManager
            animalGameManagerScript.changeablePlant = clickedPlant; // Assign the clicked plant to the AnimalGameManager script
            animalGameManagerScript.PurpleLoosestrifeClicked(clickedPlant);
            ObjectManager.instance.SetObjectFound("Purple Loosestrife"); // Set the object as found in the ObjectManager
        }
        else
        {
            Debug.LogWarning("Clicked object does not have a ChangeablePlant component.");
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

    // Handles clicks on event animals
    private void EventAnimalClicked()
    {
        Debug.Log("Event animal clicked.");
        eventAnimalClicked = true;
    }
}
