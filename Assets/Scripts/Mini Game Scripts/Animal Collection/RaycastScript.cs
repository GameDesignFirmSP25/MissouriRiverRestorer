using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

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
    private string[] muskeratNames;

    [SerializeField]
    private string[] snappingTurtleNames;

    [SerializeField]
    private string[] northernMapTurtleNames;

    [Header("Camera")]
    public Camera _mainCamera;

    [Header("Raycast Variables")]
    private Ray ray;
    private RaycastHit hit;

    [Header("Script References")]
    public AnimalGameManager animalGameManagerScript;

    [Header("Booleans")]
    public static bool easternStarlingClicked = false;
    public static bool whiteTailedDeerClicked = false;
    public static bool bandedPennantDragonflyClicked = false;
    public static bool garterSnakeClicked = false;
    public static bool baldEagleClicked = false;
    public static bool paintedLadyButterflyClicked = false;
    public static bool asianCarpClicked = false;
    public static bool beaverClicked = false;
    public static bool raccoonClicked = false;
    public static bool muskeratClicked = false;
    public static bool snappingTurtleClicked = false;
    public static bool northernMapTurtleClicked = false;

    [Header("Layers to Hit")]
    public LayerMask layersToHit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set list of eastern starling names
        easternStarlingNames = new string[]
        {
            "Eastern Starling", "Eastern Starling (1)", "Eastern Starling (2)",
            "Eastern Starling (3)", "Eastern Starling (4)", "Eastern Starling (5)",
            "Eastern Starling (6)", "Eastern Starling (7)", "Eastern Starling (8)",
            "Eastern Starling (9)", "Eastern Starling (10)"
        };

        // Set list of white-tailed deer names
        whiteTailedDeerNames = new string[]
        {
            "White-Tailed Deer", "White-Tailed Deer (1)", "White-Tailed Deer (2)",
            "White-Tailed Deer (3)"
        };

        // Set list of banded pennant dragonfly names
        bandedPennantDragonflyNames = new string[]
        {
            "Banded Pennant Dragonfly", "Banded Pennant Dragonfly (1)", "Banded Pennant Dragonfly (2)",
            "Banded Pennant Dragonfly (3)", "Banded Pennant Dragonfly (4)", "Banded Pennant Dragonfly (5)",
            "Banded Pennant Dragonfly (6)"
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
            "Bald Eagle", "Bald Eagle (1)", "Bald Eagle (2)",
            "Bald Eagle (3)"
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
            "Asian Carp (3)", "Asian Carp (4)"
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
            "Raccoon (3)"
        };

        // Set list of muskrat names
        muskeratNames = new string[]
        {
            "Muskrat", "Muskrat (1)", "Muskrat (2)",
            "Muskrat (3)"
        };

        // Set list of snapping turtle names
        snappingTurtleNames = new string[]
        {
            "Snapping Turtle", "Snapping Turtle (1)", "Snapping Turtle (2)",
            "Snapping Turtle (3)"
        };

        // Set list of northern map turtle names
        northernMapTurtleNames = new string[]
        {
            "Northern Map Turtle", "Northern Map Turtle (1)", "Northern Map Turtle (2)",
            "Northern Map Turtle (3)"
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Detect left mouse click.
        if (Input.GetMouseButtonDown(0))
        {
            CastRay(); // Call the CastRay function
        }
    }

    // Method to cast a ray from the camera to the mouse position
    void CastRay()
    {
        ray = _mainCamera.ScreenPointToRay(Input.mousePosition); // Create a ray from the camera to the mouse position
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToHit)) // Perform the raycast
        {
            //            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red); // Draw the ray for debugging purposes

            // If the clicked GameObject's name matches the target name for eastern starling...
            if (System.Array.Exists(easternStarlingNames, name => name == hit.collider.gameObject.name))
            {
                HandleEasternStarlingClick(hit.collider.gameObject); // Handle the click on the eastern starling GameObject
            }
            // Check if the clicked GameObject's name matches any white-tailed deer name
            else if (System.Array.Exists(whiteTailedDeerNames, name => name == hit.collider.gameObject.name))
            {
                HandleWhiteTailedDeerClick(hit.collider.gameObject); // Handle the click on the white-tailed deer GameObject
            }
            // Check if the clicked GameObject's name matches any banded pennant dragonfly name
            else if (System.Array.Exists(bandedPennantDragonflyNames, name => name == hit.collider.gameObject.name))
            {
                HandleBandedPennantDragonflyClick(hit.collider.gameObject); // Handle the click on the banded pennant dragonfly GameObject
            }
            else if (System.Array.Exists(garterSnakeNames, name => name == hit.collider.gameObject.name))
            {
                HandleGarterSnakeClick(hit.collider.gameObject); // Handle the click on the garter snake GameObject
            } 
            else if (System.Array.Exists(baldEagleNames, name => name == hit.collider.gameObject.name))
            {
                HandleBaldEagleClick(hit.collider.gameObject); // Handle the click on the bald eagle GameObject 
            }
            else if (System.Array.Exists(paintedLadyButterflyNames, name => name == hit.collider.gameObject.name))
            {
                HandlePaintedLadyButterflyClick(hit.collider.gameObject); // Handle the click on the painted lady butterfly GameObject 
            }
            else if (System.Array.Exists(asianCarpNames, name => name == hit.collider.gameObject.name))
            {
                HandleAsianCarpClick(hit.collider.gameObject); // Handle the click on the asian carp GameObject 
            }
            else if (System.Array.Exists(beaverNames, name => name == hit.collider.gameObject.name))
            {
                HandleBeaverClick(hit.collider.gameObject); // Handle the click on the beaver GameObject 
            }
            else if (System.Array.Exists(raccoonNames, name => name == hit.collider.gameObject.name))
            {
                HandleRaccoonClick(hit.collider.gameObject); // Handle the click on the raccoon GameObject 
            }
            else if (System.Array.Exists(muskeratNames, name => name == hit.collider.gameObject.name))
            {
                HandleMuskratClick(hit.collider.gameObject); // Handle the click on the muskrat GameObject 
            }
            else if (System.Array.Exists(snappingTurtleNames, name => name == hit.collider.gameObject.name))
            {
                HandleSnappingTurtleClick(hit.collider.gameObject); // Handle the click on the snapping turtle GameObject 
            }
            else if (System.Array.Exists(northernMapTurtleNames, name => name == hit.collider.gameObject.name))
            {
                HandleNorthernMapTurtleClick(hit.collider.gameObject); // Handle the click on the northern map turtle GameObject 
            }
        }
    }

    // Handle clicks on eastern starling
    private void HandleEasternStarlingClick(GameObject clickedObject)
    {
        // If bool easternStarlingClicked is false...
        if (!easternStarlingClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            easternStarlingClicked = true; // Set bool easternStarlingClicked to true
        }
        else
        {
            Debug.Log("Eastern Starling has already been clicked."); // Debug.Log
        }
    }

    // Handle clicks on white-tailed deer
    private void HandleWhiteTailedDeerClick(GameObject clickedObject)
    {
        // If bool whiteTailedDeerClicked is false...
        if (!whiteTailedDeerClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            whiteTailedDeerClicked = true; // Set bool whiteTailedDeerClicked to true
        }
        else
        {
            Debug.Log("White-tailed Deer has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on banded pennant dragonfly
    private void HandleBandedPennantDragonflyClick(GameObject clickedObject)
    {
        // If bool bandedPennantDragonflyClicked is false...
        if (!bandedPennantDragonflyClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            bandedPennantDragonflyClicked = true; // Set bool bandedPennantDragonflyClicked to true
        }
        else
        {
            Debug.Log("Banded Pennant Dragonfly has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on garter snake
    private void HandleGarterSnakeClick(GameObject clickedObject)
    {
        // If bool garterSnakeClicked is false...
        if (!garterSnakeClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            garterSnakeClicked = true; // Set bool garterSnakeClicked to true
        }
        else
        {
            Debug.Log("Garter Snake has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on bald eagle
    private void HandleBaldEagleClick(GameObject clickedObject)
    {
        // If bool baldEagleClicked is false...
        if (!baldEagleClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            baldEagleClicked = true; // Set bool baldEagleClicked to true
        }
        else
        {
            Debug.Log("Bald Eagle has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on bald eagle
    private void HandlePaintedLadyButterflyClick(GameObject clickedObject)
    {
        // If bool baldEagleClicked is false...
        if (!paintedLadyButterflyClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            paintedLadyButterflyClicked = true; // Set bool baldEagleClicked to true
        }
        else
        {
            Debug.Log("Painted Lady Butterfly has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on asian carp
    private void HandleAsianCarpClick(GameObject clickedObject)
    {
        // If bool asianCarpClicked is false...
        if (!asianCarpClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            asianCarpClicked = true; // Set bool asianCarpClicked to true
        }
        else
        {
            Debug.Log("Asian Carp has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on beaver
    private void HandleBeaverClick(GameObject clickedObject)
    {
        // If bool beaverClicked is false...
        if (!beaverClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            beaverClicked = true; // Set bool beaverClicked to true
        }
        else
        {
            Debug.Log("Beaver has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on raccoon
    private void HandleRaccoonClick(GameObject clickedObject)
    {
        // If bool raccoonClicked is false...
        if (!raccoonClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            raccoonClicked = true; // Set bool raccoonClicked to true
        }
        else
        {
            Debug.Log("Raccoon has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on muskrat
    private void HandleMuskratClick(GameObject clickedObject)
    {
        // If bool muskeratClicked is false...
        if (!muskeratClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            muskeratClicked = true; // Set bool muskeratClicked to true
        }
        else
        {
            Debug.Log("Muskrat has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on snapping turtle
    private void HandleSnappingTurtleClick(GameObject clickedObject)
    {
        // If bool snappingTurtleClicked is false...
        if (!snappingTurtleClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            snappingTurtleClicked = true; // Set bool snappingTurtleClicked to true
        }
        else
        {
            Debug.Log("Snapping Turtle has already been clicked."); // Debug.Log
        }
    }

    // Handles clicks on northern map turtle
    private void HandleNorthernMapTurtleClick(GameObject clickedObject)
    {
        // If bool northernMapTurtleClicked is false...
        if (!northernMapTurtleClicked)
        {
            Debug.Log($"GameObject {clickedObject.name} was clicked!"); // Debug.Log
            northernMapTurtleClicked = true; // Set bool northernMapTurtleClicked to true
        }
        else
        {
            Debug.Log("Northern Map Turtle has already been clicked."); // Debug.Log
        }
    }
}
