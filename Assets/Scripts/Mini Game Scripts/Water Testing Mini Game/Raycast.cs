using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Raycast : MonoBehaviour
{
    [SerializeField]
    private GameObject effectsOfGasPanel;

    [SerializeField]
    private GameObject effectsOfTrashPanel;

    [SerializeField]
    private GameObject effectsOfTirePanel;

    [SerializeField]
    private GameObject effectsOfAluminumPanel;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel1;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel2;

    [SerializeField]
    private GameObject effectsOfBiodiversityPanel3;

    public Camera _mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public WaterTestingManager waterTestingManagerScript;

    public bool testTubeClicked;
    public static bool aPanelIsActive;
    public static bool isClickable;

    private void Start()
    {
        aPanelIsActive = false; // Set bool aPanelIsActive to false
        isClickable = false; // Set bool isClickable to false
        testTubeClicked = false; // Set bool testTubeClicked to false
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

    // Function to handle the Test Tube click event
    void TestTubeClicked()
    {
        Debug.Log("Test Tube clicked"); // Debug.Log
        testTubeClicked = true; // Set bool riverClicked to true
        Destroy(hit.collider.gameObject); // Destroy the object that was clicked
    }

    // Function to handle the Aluminum Can click event
    void AluminumCanClicked()
    {
        Debug.Log("Aluminum can clicked"); // Debug.Log
        WaterTestingManager.effectsOfAluminumPanelActive = true; // Set bool effectsOfTrashPanelActive to true
        effectsOfAluminumPanel.SetActive(true); // Set the effectsOfTrashPanel to active
        WaterTestingManager.isAluminumCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Tire click event
    void TireClicked()
    {
        Debug.Log("Tire clicked"); // Debug.Log
        WaterTestingManager.effectsOfTirePanelActive = true; // Set bool effectsOfTirePanelActive to true
        effectsOfTirePanel.SetActive(true); // Set the effectsOfTirePanel to active
        WaterTestingManager.isTireObjectiveComplete = true; // Set bool isTireObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Gas Can click event
    void GasCanisterClicked()
    {
        Debug.Log("Gas canister clicked"); // Debug.Log
        WaterTestingManager.effectsOfGasPanelActive = true; // Set bool effectsOfGasPanelActive to true
        effectsOfGasPanel.SetActive(true); // Set the effectsOfGasPanel to active
        WaterTestingManager.isGasCanObjectiveComplete = true; // Set bool isGasCanObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Trash Bag click event
    void TrashBagClicked()
    {
        Debug.Log("Trash bag clicked"); // Debug.Log
        WaterTestingManager.effectsOfTrashPanelActive = true; // Set bool effectsOfTrashPanelActive to true
        effectsOfTrashPanel.SetActive(true); // Set the effectsOfTrashPanel to active
        WaterTestingManager.isTrashBagObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Fish click event
    void FishClicked()
    {
        Debug.Log("Fish clicked"); // Debug.Log
        WaterTestingManager.effectsOfBiodiversity1PanelActive = true; // Set bool effectsOfFishPanelActive to true
        effectsOfBiodiversityPanel1.SetActive(true); // Set the effectsOfBiodiversityPanel1 to active
        WaterTestingManager.isFishObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Mammal click event
    void MammalClicked()
    {
        Debug.Log("Mammal clicked"); // Debug.Log
        WaterTestingManager.effectsOfBiodiversity3PanelActive = true; // Set bool effectsOfMammalPanelActive to true
        effectsOfBiodiversityPanel3.SetActive(true); // Set the effectsOfBiodiversityPanel2 to active
        WaterTestingManager.isMammalObjectiveComplete = true; // Set bool isMammalObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Riverbank click event
    void RiverbankClicked()
    {
        Debug.Log("Riverbank clicked"); // Debug.Log
        WaterTestingManager.effectsOfBiodiversity2PanelActive = true; // Set bool effectsOfRiverbankPanelActive to true
        effectsOfBiodiversityPanel2.SetActive(true); // Set the effectsOfBiodiversityPanel3 to active
        WaterTestingManager.isRiverbankObjectiveComplete = true; // Set bool isRiverbankObjectiveComplete to true
        aPanelIsActive = true; // Set bool aPanelIsActive to true
    }

    // Function to handle the Raycast
    void CastRay()
    {
        // Create a ray from the camera to the mouse position
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the clicked object has the "Test Tube" tag & if the objectives are complete
            if (hit.collider.CompareTag("Test Tube") && waterTestingManagerScript.objectivesComplete)

            {
                TestTubeClicked(); // Call the TestTubeClicked method
            }

            // If bool aPanelIsActive is false...
            if (!aPanelIsActive)

            {
                // If bool isFirstWaterTestCoomplete is true...
                if (!WaterTestingManager.isFirstWaterTestComplete)
                {
                    // Check if the clicked object has the "Trash: can" tag & bool isAluminumCanObjectiveComplete is false
                    if (hit.collider.CompareTag("Trash: can") && !WaterTestingManager.isAluminumCanObjectiveComplete)
                    {
                        AluminumCanClicked(); // Call the AluminumCanClicked method
                    }
                    else
                    {
                        ;
                    }

                    // Check if the clicked object has the "Trash: tire" tag & bool isTireObjectiveComplete is false
                    if (hit.collider.CompareTag("Trash: tire") && !WaterTestingManager.isTireObjectiveComplete)
                    {
                        TireClicked(); // Call the TireClicked method
                    }
                    else
                    {
                        ;
                    }

                    // Check if the clicked object has the "Trash: gas can" tag & bool isGasCanObjectiveComplete is false
                    if (hit.collider.CompareTag("Trash: gas can") && !WaterTestingManager.isGasCanObjectiveComplete)
                    {
                        GasCanisterClicked(); // Call the GasCanisterClicked method
                    }
                    else
                    {
                        ;

                    }

                    // Check if the clicked object has the "Trash: trash bag" tag & bool isTrashBagObjective is false
                    if (hit.collider.CompareTag("Trash: trash bag") && !WaterTestingManager.isTrashBagObjectiveComplete)
                    {
                        TrashBagClicked(); // Call the TrashBagClicked method
                    }
                    else
                    {
                        ;
                    }
                }

                // If bool isFirstWaterTestComplete is true & isSecondWaterTestComplete is false...
                if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
                {
                    // Check if clicked object has the "Fish" tag & bool isFishObjectiveComplete is false
                    if (hit.collider.CompareTag("Fish") && !WaterTestingManager.isFishObjectiveComplete)
                    {
                        FishClicked(); // Call the FishClicked method
                    }
                    else
                    {
                        ;
                    }

                    //Check if clicked object has the "Mammal" tag & bool isMammalObjectiveComplete is false
                    if (hit.collider.CompareTag("Mammal") && !WaterTestingManager.isMammalObjectiveComplete)
                    {
                        MammalClicked(); // Call the MammalClicked method
                    }
                    else
                    {
                        ;
                    }

                    // Check if clicked object has the "Riverbank" tag & bool isRiverbankObjectiveComplete is false
                    if (hit.collider.CompareTag("Riverbank") && !WaterTestingManager.isRiverbankObjectiveComplete)
                    {
                        RiverbankClicked(); // Call the RiverbankClicked method
                    }
                    else
                    {
                        ;
                    }
                }

            }

        }
    }
}
