using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Camera _mainCamera;

    private Ray ray;
    private RaycastHit hit;

    public bool testTubeClicked = false;
    public bool aPanelIsActive = false;
    public static bool isClickable = false;

    private void Update()
    {
        MouseClicked();
    }

    public void MouseClicked() // Raycast to detect when the mouse is clicked
    {
        if (isClickable)
        {
            // Detect left mouse click.
            if (Input.GetMouseButtonDown(0))
            {

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    // Check if the clicked object has the "Test Tube" tag & if the objectives are complete
                    if (hit.collider.CompareTag("Test Tube") && WaterTestingManager.objectivesComplete)
                    {
                        Debug.Log("Test Tube clicked"); // Debug.Log
                        testTubeClicked = true; // Set bool riverClicked to true
                        Destroy(hit.collider.gameObject); // Destroy the object that was clicked
                    }

                    
                    if (!aPanelIsActive)
                    {
                        // If bool isFirstWaterTestCoomplete is true...
                        if (!WaterTestingManager.isFirstWaterTestComplete)
                        {
                            // Check if the clicked object has the "Trash: can" tag & bool isAluminumCanObjectiveComplete is false
                            if (hit.collider.CompareTag("Trash: can") && !WaterTestingManager.isAluminumCanObjectiveComplete)
                            {
                                Debug.Log("Aluminum can clicked"); // Debug.Log
                                WaterTestingManager.effectsOfAluminumPanelActive = true; // Set bool effectsOfTrashPanelActive to true
                                effectsOfAluminumPanel.SetActive(true); // Set the effectsOfTrashPanel to active
                                WaterTestingManager.isAluminumCanObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
                            }
                            else
                            {
                                ;
                            }

                            // Check if the clicked object has the "Trash: tire" tag & bool isTireObjectiveComplete is false
                            if (hit.collider.CompareTag("Trash: tire") && !WaterTestingManager.isTireObjectiveComplete)
                            {
                                Debug.Log("Tire clicked"); // Debug.Log
                                WaterTestingManager.effectsOfTirePanelActive = true; // Set bool effectsOfTirePanelActive to true
                                effectsOfTirePanel.SetActive(true); // Set the effectsOfTirePanel to active
                                WaterTestingManager.isTireObjectiveComplete = true; // Set bool isTireObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
                            }
                            else
                            {
                                ;
                            }

                            // Check if the clicked object has the "Trash: gas can" tag & bool isGasCanObjectiveComplete is false
                            if (hit.collider.CompareTag("Trash: gas can") && !WaterTestingManager.isGasCanObjectiveComplete)
                            {
                                Debug.Log("Gas canister clicked"); // Debug.Log
                                WaterTestingManager.effectsOfGasPanelActive = true; // Set bool effectsOfGasPanelActive to true
                                effectsOfGasPanel.SetActive(true); // Set the effectsOfGasPanel to active
                                WaterTestingManager.isGasCanObjectiveComplete = true; // Set bool isGasCanObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
                            }
                            else
                            {
                                ;

                            }

                            // Check if the clicked object has the "Trash: trash bag" tag & bool isTrashBagObjective is false
                            if (hit.collider.CompareTag("Trash: trash bag") && !WaterTestingManager.isTrashBagObjectiveComplete)
                            {
                                Debug.Log("Trash bag clicked"); // Debug.Log
                                WaterTestingManager.effectsOfTrashPanelActive = true; // Set bool effectsOfTrashPanelActive to true
                                effectsOfTrashPanel.SetActive(true); // Set the effectsOfTrashPanel to active
                                WaterTestingManager.isTrashBagObjectiveComplete = true; // Set bool isTrashBagObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
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
                                Debug.Log("Fish clicked"); // Debug.Log
                                WaterTestingManager.effectsOfBiodiversity1PanelActive = true; // Set bool effectsOfFishPanelActive to true
                                WaterTestingManager.isFishObjectiveComplete = true; // Set bool isFishObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
                            }
                            else
                            {
                                ;
                            }

                            //Check if clicked object has the "Mammal" tag & bool isMammalObjectiveComplete is false
                            if (hit.collider.CompareTag("Mammal") && !WaterTestingManager.isMammalObjectiveComplete)
                            {
                                Debug.Log("Mammal clicked"); // Debug.Log
                                WaterTestingManager.effectsOfBiodiversity2PanelActive = true; // Set bool effectsOfMammalPanelActive to true
                                WaterTestingManager.isMammalObjectiveComplete = true; // Set bool isMammalObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
                            }
                            else
                            {
                                ;
                            }

                            // Check if clicked object has the "Riverbank" tag & bool isRiverbankObjectiveComplete is false
                            if (hit.collider.CompareTag("Riverbank") && !WaterTestingManager.isRiverbankObjectiveComplete)
                            {
                                Debug.Log("Riverbank clicked"); // Debug.Log
                                WaterTestingManager.effectsOfBiodiversity3PanelActive = true; // Set bool effectsOfRiverbankPanelActive to true
                                WaterTestingManager.isRiverbankObjectiveComplete = true; // Set bool isRiverbankObjectiveComplete to true
                                aPanelIsActive = true; // Set bool aPanelIsActive to true
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
    }
}
