using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    public Camera _mainCamera;

    public GameObject clickedObject; // Variable to store the clicked object

    [Header("Float Variables")]
    private float raycastDistance = 12f;

    private Ray ray;
    private RaycastHit hit;

    public WaterTestingManager waterTestingManagerScript;

    public static bool surfaceWaveClicked;
    public static bool isClickable;

    private void Start()
    {
        isClickable = false; // Set bool isClickable to false
        //testTubeClicked = false; // Set bool testTubeClicked to false
    }

    private void Update()
    {
        MouseClicked(); // Call the MouseClicked function
    }

    // Function to detect mouse clicks and perform raycasting
    public void MouseClicked() 
    {
        if (isClickable)
        {
            // Detect left mouse click.
            if (Input.GetMouseButtonDown(0))
            {
                // Check if the mouse is over a UI element
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("Pointer is over a UI element.  Not raycasting."); // Debug.Log
                    // Do something else or nothing
                }
                else
                {
                    CastRay(); // Call the CastRay function

                }
            }
        }
    }

    //// Function to handle the Test Tube click event
    //void TestTubeClicked()
    //{
    //    Debug.Log("Test Tube clicked"); // Debug.Log
    //    testTubeClicked = true; // Set bool riverClicked to true
    //    Destroy(hit.collider.gameObject); // Destroy the object that was clicked
    //}

    void SurfaceWaveClicked()
    {
        Debug.Log("Surface wave clicked"); // Debug.Log
        surfaceWaveClicked = true; // Set bool surfaceWaveClicked to true  
    }
    
    // Function to handle the Aluminum Can click event
    void AluminumCanClicked()
    {
        Debug.Log("Aluminum can clicked"); // Debug.Log

        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfAluminumPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isAluminumCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Function to handle the Tire click event
    void TireClicked()
    {
        Debug.Log("Tire clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTirePanelActive = true; // Set bool effectsOfTirePanelActive to true
            WaterTestingManager.isTireObjectiveComplete = true; // Set bool isTireObjectiveComplete to true
        }
    }

    // Function to handle the Gas Can click event
    void GasCanisterClicked()
    {
        Debug.Log("Gas canister clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfGasPanelActive = true; // Set bool effectsOfGasPanelActive to true
            WaterTestingManager.isGasCanObjectiveComplete = true; // Set bool isGasCanObjectiveComplete to true
        }
    }

    // Function to handle the Trash Bag click event
    void TrashBagClicked()
    {
        Debug.Log("Trash bag clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfTrashPanelActive = true; // Set bool effectsOfTrashPanelActive to true
            WaterTestingManager.isTrashBagObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        }
    }

    // Function to handle the Fish click event
    void FishClicked()
    {
        Debug.Log("Fish clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity1PanelActive = true; // Set bool effectsOfFishPanelActive to true
            WaterTestingManager.isFishObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
        }
    }

    // Function to handle the Mammal click event
    void MammalClicked()
    {
        Debug.Log("Mammal clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity3PanelActive = true; // Set bool effectsOfMammalPanelActive to true
            WaterTestingManager.isMammalObjectiveComplete = true; // Set bool isMammalObjectiveComplete to true
        }
    }

    // Function to handle the Riverbank click event
    void RiverbankClicked()
    {
        Debug.Log("Riverbank clicked"); // Debug.Log
        
        if (!WaterTestingManager.aPanelIsActive)
        {
            WaterTestingManager.effectsOfBiodiversity2PanelActive = true; // Set bool effectsOfRiverbankPanelActive to true
            WaterTestingManager.isRiverbankObjectiveComplete = true; // Set bool isRiverbankObjectiveComplete to true
        }
    }

    // Function to handle the Raycast
    void CastRay()
    {
        // Create a ray from the camera to the mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            clickedObject = hit.collider.gameObject; // Get the clicked object
            Debug.Log($"Clicked on object: {clickedObject.name}");

            // Check if the clicked object has the "Test Tube" tag & if the objectives are complete
            if (clickedObject.CompareTag("Surface Wave") && waterTestingManagerScript.objectivesComplete)

            {
                SurfaceWaveClicked(); // Call the SurfaceWaveClicked method
            }

            // If bool isFirstWaterTestCoomplete is true...
            if (!WaterTestingManager.isFirstWaterTestComplete)
            {
                // Check if the clicked object has the "Trash: can" tag & bool isAluminumCanObjectiveComplete is false
                if (clickedObject.CompareTag("Trash: can") && !WaterTestingManager.isAluminumCanObjectiveComplete)
                {
                    AluminumCanClicked(); // Call the AluminumCanClicked method
                }
                
                // Check if the clicked object has the "Trash: tire" tag & bool isTireObjectiveComplete is false
                if (clickedObject.CompareTag("Trash: tire") && !WaterTestingManager.isTireObjectiveComplete)
                {
                    TireClicked(); // Call the TireClicked method
                }
                
                // Check if the clicked object has the "Trash: gas can" tag & bool isGasCanObjectiveComplete is false
                if (clickedObject.CompareTag("Trash: gas can") && !WaterTestingManager.isGasCanObjectiveComplete)
                {
                    GasCanisterClicked(); // Call the GasCanisterClicked method
                }
                
                // Check if the clicked object has the "Trash: trash bag" tag & bool isTrashBagObjective is false
                if (clickedObject.CompareTag("Trash: trash bag") && !WaterTestingManager.isTrashBagObjectiveComplete)
                {
                    TrashBagClicked(); // Call the TrashBagClicked method
                }
            }

            // If bool isFirstWaterTestComplete is true & isSecondWaterTestComplete is false...
            if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
            {
                // Check if clicked object has the "Fish" tag & bool isFishObjectiveComplete is false
                if (clickedObject.CompareTag("Fish") && !WaterTestingManager.isFishObjectiveComplete)
                {
                    FishClicked(); // Call the FishClicked method
                }
                
                //Check if clicked object has the "Mammal" tag & bool isMammalObjectiveComplete is false
                if (clickedObject.CompareTag("Mammal") && !WaterTestingManager.isMammalObjectiveComplete)
                {
                    MammalClicked(); // Call the MammalClicked method
                }

                // Check if clicked object has the "Riverbank" tag & bool isRiverbankObjectiveComplete is false
                if (clickedObject.CompareTag("Riverbank") && !WaterTestingManager.isRiverbankObjectiveComplete)
                {
                    RiverbankClicked(); // Call the RiverbankClicked method
                }
                
            }

        }
    }
}
