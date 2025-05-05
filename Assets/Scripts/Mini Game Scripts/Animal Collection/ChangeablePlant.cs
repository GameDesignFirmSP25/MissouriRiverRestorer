using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEditor;
using System.Drawing;


public class ChangeablePlant : MonoBehaviour
{
    [Header("Booleans")]
    public bool isSwapped = false;
    public static bool correctPlantSwappedPanelShown = false; // Flag to track if the correct plant swapped panel is shown
    public static bool incorrectPlantSwappedPanelShown = false; // Flag to track if the incorrect plant swapped panel is shown

    [Header("Game Objects")]
    public GameObject[] originalPlant;
    public GameObject[] swappedPlant;
    public GameObject[] plantVariants;
    public GameObject changeablePlant; 

    [Header("String Values")]
    private string childName1 = "Bradford Pear Tree";
    private string childName2 = "Purple Loosestrife";
    public string plantID;
    public string plantLocation;

    [Header("Script References")]
    public AnimalGameManager animalGameManager;

    [Header("Input Handling")]
    public StarterAssetsInputs playerInput; // Reference to the StarterAssetsInputs script for player input handling

    [SerializeField]
    private GameObject correctPlantSwappedPanel;

    [SerializeField]
    private GameObject incorrectPlantSwappedPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log($"Initializing ChangeablePlant for {gameObject.name}");
        //for (int i = 0; i < originalPlant.Length; i++)
        //{
        //    Debug.Log($"originalPlant[{i}] = {originalPlant[i]?.name ?? "null"}");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwapPlants()
    {
        // If bool isSwapped is true...
        if (isSwapped)
        {
            Debug.LogWarning($"Plant {plantID} has already been swapped."); // Debug.Log
            return;
        }

        // Ensure the original and swapped plants are properly assigned
        // If originalPlant or swappedPlant arrays are empty...
        if (originalPlant.Length == 0 || swappedPlant.Length == 0)
        {
            Debug.LogError($"Plant {plantID} does not have valid original or swapped plants assigned."); // Debug.Log
            return;
        }

        if (!isSwapped)
        {
            // Find the index of the clicked object in the originalPlant array
            int originalPlantIndex = System.Array.IndexOf(originalPlant, gameObject);

            // If the object is found in the array
            if (originalPlantIndex >= 0) 
            {
                //Debug.Log($"Clicked on plant at index {originalPlantIndex}"); // Debug.Log

                // Perform an action based on the index
                if (originalPlantIndex == 0)
                {
                    ChangeablePlant plantComponent = originalPlant[0].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 1"
                    if (plantComponent.plantID == "Bradford Pear Tree 1")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // If the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            //Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the second Bradford Pear Tree
                else if (originalPlantIndex == 1)
                {
                    ChangeablePlant plantComponent = originalPlant[1].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 2"
                    if (plantComponent.plantID == "Bradford Pear Tree 2")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // If the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the third Bradford Pear Tree
                else if (originalPlantIndex == 2)
                {
                    ChangeablePlant plantComponent = originalPlant[2].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 3"
                    if (plantComponent.plantID == "Bradford Pear Tree 3")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the fourth Bradford Pear Tree
                else if (originalPlantIndex == 3)
                {
                    ChangeablePlant plantComponent = originalPlant[3].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 4"
                    if (plantComponent.plantID == "Bradford Pear Tree 4")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // If the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the fifth Bradford Pear Tree
                else if (originalPlantIndex == 4)
                {
                    ChangeablePlant plantComponent = originalPlant[4].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 5"
                    if (plantComponent.plantID == "Bradford Pear Tree 5")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // If the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the sixth Bradford Pear Tree
                else if (originalPlantIndex == 5)
                {
                    ChangeablePlant plantComponent = originalPlant[5].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Bradford Pear Tree 6"
                    if (plantComponent.plantID == "Bradford Pear Tree 6")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName1); // Find the child transform with the name "Bradford Pear Tree"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Bradford Pear Tree"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with Sycamore button was clicked...
                        if (animalGameManager.wasReplaceWithSycamoreButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[0], childPosition, childRotation); // Instantiate the Sycamore Tree prefab as a child of the changeablePlant
                        }

                        // If the replace with Box Elder button was clicked...
                        else if (animalGameManager.wasReplaceWithBoxElderButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[1], childPosition, childRotation); // Instantiate the Box Elder Tree prefab as a child of the changeablePlant
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }

                        animalGameManager.BradfordPearsSwapped += 1; // Increment the number of Bradford Pears swapped
                        animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                        animalGameManager.UpdateBradfordPearsSwappedCounter(); // Update the Bradford Pears swapped counter
                        animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                    }
                    else
                    {
                        Debug.LogWarning("Clicked plant is not a Bradford Pear Tree."); // Debug.Log
                        return;
                    }
                }

                // If the clicked plant is the first Purple Loosestrife...
                // plant location: upper bank
                else if (originalPlantIndex == 6)
                {
                    ChangeablePlant plantComponent = originalPlant[6].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Purple Loosestrife 1"
                    if (plantComponent.plantID == "Purple Loosestrife 1")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2); // Find the child transform with the name "Purple Loosestrife"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Purple Loosestrife"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with American Lotus button was clicked...
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8); // Randomly select an index for the American Lotus Flower
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex]; // Get the selected plant from the swappedPlant array
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation); // Instantiate the selected plant prefab as a child of the changeablePlant

                            Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("American Lotus", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter

                        }

                        // If the replace with Cordgrass button was clicked...
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10); // Randomly select an index for the Cordgrass
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex]; // Get the selected plant from the swappedPlant array
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation); // Instantiate the selected plant prefab as a child of the changeablePlant

                            Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Cordgrass", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }

                        // If the replace the Swamp Milkweed button was clicked...
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation); // Instantiate the Swamp Milkweed prefab as a child of the changeablePlant

                            Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[10]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Swamp Milkweed", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }

                        // If th replace with Yellow Coneflower button as clicked...
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            Debug.Log("Correct plant selected. Spawning multiple yellow coneflowers..."); // Debug.Log
                            // Spawn multiple yellow coneflowers around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }
                    }
                }

                // If the clicked plant is the second Purple Loosestrife...
                else if (originalPlantIndex == 7)
                {
                    ChangeablePlant plantComponent = originalPlant[7].GetComponent<ChangeablePlant>(); // Get the ChangeablePlant component from the clicked object

                    // If the plantID matches "Purple Loosestrife 2"
                    if (plantComponent.plantID == "Purple Loosestrife 2")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2); // Find the child transform with the name "Purple Loosestrife"
                        Vector3 childPosition = childTransform.position; // Get the position of the child transform
                        Quaternion childRotation = childTransform.rotation; // Get the rotation of the child transform
                        //Debug.Log("Destroying Purple Loosestrife"); // Debug.Log
                        Destroy(childTransform.gameObject); // Destroy the child game object
                        //Debug.Log("Swapping Plants..."); // Debug.Log
                        isSwapped = true; // Set the plant as swapped

                        // If the replace with American Lotus button was clicked...
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8); // Randomly select an index for the American Lotus Flower
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex]; // Get the selected plant from the swappedPlant array
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation); // Instantiate the selected plant prefab as a child of the changeablePlant

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("American Lotus", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }

                        // If the replace with Cordgrass button was clicked...
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10); // Randomly select an index for the Cordgrass
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex]; // Get the selected plant from the swappedPlant array
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation); // Instantiate the selected plant prefab as a child of the changeablePlant

                            //Debug.Log("Correct plant selected. Spawning multiple cordgrass..."); // Debug.Log
                            // Spawn multiple cordgrass around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject growingPlant = swappedPlant[Random.Range(8, 10)]; // growingPlant is selectig a random variant of Cordgrass
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(growingPlant, childPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }

                        // If the replace the Swamp Milkweed button was clicked...
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation); // Instantiate the Swamp Milkweed prefab as a child of the changeablePlant

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[10]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Swamp Milkweed", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple yellow coneflowers..."); // Debug.Log
                            // Spawn multiple yellow coneflowers around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }

                        Collider collider = GetComponent<Collider>(); // Get the Collider component of the clicked object

                        // If the collider is not null and the game object of the collider is the same as the clicked object...
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false; // Disable the collider
                            //Debug.Log($"Collider disabled for {plantID}"); // Debug.Log
                        }
                    }
                }
                // If the clicked plant is the third Purple Loosestrife...
                else if (originalPlantIndex == 8)
                {
                    ChangeablePlant plantComponent = originalPlant[8].GetComponent<ChangeablePlant>();
                    if (plantComponent.plantID == "Purple Loosestrife 3")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2);
                        Vector3 childPosition = childTransform.position;
                        Quaternion childRotation = childTransform.rotation;
                        //Debug.Log("Destroying Purple Loosestrife");
                        Destroy(childTransform.gameObject);
                        //Debug.Log("Swapping Plants...");
                        isSwapped = true; // Set the plant as swapped
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple american lotus..."); // Debug.Log
                            // Spawn multiple american lotus around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject growingPlant = swappedPlant[Random.Range(2, 8)]; // growingPlant is selectig a random variant of american lotus
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(growingPlant, childPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Cordgrass", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple swamp milkweed..."); // Debug.Log
                            // Spawn multiple swamp milkweed around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(swappedPlant[10], childPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[11]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Yellow Coneflower", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }

                        Collider collider = GetComponent<Collider>();
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false;
                            //Debug.Log($"Collider disabled for {plantID}");
                        }
                    }
                }
                else if (originalPlantIndex == 9)
                {
                    ChangeablePlant plantComponent = originalPlant[9].GetComponent<ChangeablePlant>();
                    if (plantComponent.plantID == "Purple Loosestrife 4")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2);
                        Vector3 childPosition = childTransform.position;
                        Quaternion childRotation = childTransform.rotation;
                        //Debug.Log("Destroying Purple Loosestrife");
                        Destroy(childTransform.gameObject);
                        //Debug.Log("Swapping Plants...");
                        isSwapped = true; // Set the plant as swapped
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("American Lotus", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple cordgrass..."); // Debug.Log
                            // Spawn multiple cordgrass around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject growingPlant = swappedPlant[Random.Range(8, 10)]; // growingPlant is selectig a random variant of cordgrass
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(growingPlant, childPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[10]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Swamp Milkweed", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[11]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Yellow Coneflower", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }

                        Collider collider = GetComponent<Collider>();
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false;
                            Debug.Log($"Collider disabled for {plantID}");
                        }
                    }
                }
                else if (originalPlantIndex == 10)
                {
                    ChangeablePlant plantComponent = originalPlant[10].GetComponent<ChangeablePlant>();
                    if (plantComponent.plantID == "Purple Loosestrife 5")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2);
                        Vector3 childPosition = childTransform.position;
                        Quaternion childRotation = childTransform.rotation;
                        //Debug.Log("Destroying Purple Loosestrife");
                        Destroy(childTransform.gameObject);
                        //Debug.Log("Swapping Plants...");
                        isSwapped = true; // Set the plant as swapped
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("American Lotus", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple cordgrass..."); // Debug.Log
                            // Spawn multiple cordgrass around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                GameObject growingPlant = swappedPlant[Random.Range(8, 10)]; // growingPlant is selectig a random variant of cordgrass
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(growingPlant, childPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[10]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Swamp Milkweed", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple yellow coneflowers..."); // Debug.Log
                            // Spawn multiple yellow coneflowers around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }

                        Collider collider = GetComponent<Collider>();
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false;
                            //Debug.Log($"Collider disabled for {plantID}");
                        }
                    }
                }
                else if (originalPlantIndex == 11)
                {
                    ChangeablePlant plantComponent = originalPlant[11].GetComponent<ChangeablePlant>();
                    if (plantComponent.plantID == "Purple Loosestrife 6")
                    {
                        Transform childTransform = changeablePlant.transform.Find(childName2);
                        Vector3 childPosition = childTransform.position;
                        Quaternion childRotation = childTransform.rotation;
                        //Debug.Log("Destroying Purple Loosestrife");
                        Destroy(childTransform.gameObject);
                        //Debug.Log("Swapping Plants...");
                        isSwapped = true; // Set the plant as swapped
                        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(2, 8);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("American Lotus", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
                        {
                            int swappedPlantIndex = Random.Range(8, 10);
                            GameObject selectedPlant = swappedPlant[swappedPlantIndex];
                            InstantiatePrefabAsChild(selectedPlant, childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int wiltingPlantIndex = swappedPlantIndex - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Cordgrass", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
                        {
                            InstantiatePrefabAsChild(swappedPlant[10], childPosition, childRotation);

                            //Debug.Log("Incorrect plant selected. Wilting plant..."); // Debug.Log
                            //Wilt the plant by spawning in its wilting variant
                            int index = System.Array.IndexOf(swappedPlant, swappedPlant[10]); // Get the index of the Swamp Milkweed in the swappedPlant array
                            int wiltingPlantIndex = index - 2;
                            GameObject wiltingPlant = plantVariants[wiltingPlantIndex]; // Get the wilting variant of the plant
                            ReplaceChildWithVariant("Swamp Milkweed", wiltingPlant);

                            if (!incorrectPlantSwappedPanelShown) // If the incorrect plant swapped panel is not shown yet...
                            {
                                //Debug.Log("Showing incorrect plant swapped panel..."); // Debug.Log
                                incorrectPlantSwappedPanel.SetActive(true); // Show the incorrect plant swapped panel
                                incorrectPlantSwappedPanelShown = true; // Set the flag to indicate that the incorrect plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                        }
                        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
                        {
                            Vector3 adjustedPosition = childPosition + new Vector3(0.35f, 0, 1.67f); // Adjust the position to bring flower closer to normal of its parent
                            InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition, childRotation);

                            //Debug.Log("Correct plant selected. Spawning multiple yellow coneflowers..."); // Debug.Log
                            // Spawn multiple yellow coneflowers around the instantiated plant
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(0.5f, 1.5f);
                                InstantiatePrefabAsChild(swappedPlant[11], adjustedPosition + randomOffset, childRotation);
                            }

                            if (!correctPlantSwappedPanelShown)
                            {
                                //Debug.Log("Showing correct plant swapped panel...");
                                correctPlantSwappedPanel.SetActive(true); // Show the correct plant swapped panel
                                correctPlantSwappedPanelShown = true; // Set the flag to indicate that the correct plant swapped panel is shown
                                playerInput.controlsLocked = true; // Unlock player controls when the panel is clicked
                            }

                            animalGameManager.PurpleLoosestrifesSwapped += 1; // Increment the number of Purple Loosestrifes swapped
                            animalGameManager.PlantsCorrectlySwapped += 1; // Increment the number of plants correctly swapped
                            animalGameManager.UpdatePurpleLoosestrifesSwappedCounter(); // Update the Purple Loosestrifes swapped counter
                            animalGameManager.UpdatePlantsCorrectlySwappedCounter(); // Update the plants correctly swapped counter
                        }

                        Collider collider = GetComponent<Collider>();
                        if (collider != null && collider.gameObject == gameObject)
                        {
                            collider.enabled = false;
                            //Debug.Log($"Collider disabled for {plantID}");
                        }
                    }
                }
                else
                {
                    //Debug.Log("Clicked object is not in the originalPlant array.");
                }
            }
        }
        
    }

    public void InstantiatePrefabAsChild(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        float randomYRotation = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(rotation.eulerAngles.x, randomYRotation, rotation.eulerAngles.z);

        // Instantiate the prefab
        GameObject instantiatedPrefab = Instantiate(prefab, position, rotation);

        // Set the parent of the instantiated prefab
        instantiatedPrefab.transform.SetParent(transform);

        // Update the name of the instantiated child based on the selected plant type
        if (animalGameManager.wasReplaceWithAmericanLotusButtonClicked)
        {
            instantiatedPrefab.name = "American Lotus"; 
        }
        else if (animalGameManager.wasReplaceWithCordgrassButtonClicked)
        {
            instantiatedPrefab.name = "Cordgrass";
        }
        else if (animalGameManager.wasReplaceWithSwampMilkweedButtonClicked)
        {
            instantiatedPrefab.name = "Swamp Milkweed";
        }
        else if (animalGameManager.wasReplaceWithYellowConeflowerButtonClicked)
        {
            instantiatedPrefab.name = "Yellow Coneflower";
        }

        //Debug.Log($"Name changed to {instantiatedPrefab.name}");
        childName2 = instantiatedPrefab.name; // Update the name of the child object
        //Debug.Log($"childName2 updated to: {childName2}");
        isSwapped = false;

        //Debug.Log($"Prefab instantiated as a child at {position} with rotation {rotation}");
    }

    public void ReplaceChildWithVariant (string targetPlantID, GameObject prefabVariant)
    {
        //Debug.Log($"Target Plant ID: {targetPlantID}");
        // Check if the parent's plantID matches the targetPlantID
        if (childName2 == targetPlantID)
        {
            //Debug.Log($"Parent with plantID {targetPlantID} found. Replacing child with {prefabVariant.name}");

            // Iterate through all child objects of changeablePlant
            foreach (Transform child in changeablePlant.transform)
            {
                if (child.name == childName2)
                {
                    //Debug.Log($"Deactivating child: {child.name}");
                    child.gameObject.SetActive(false); // Deactivate the child

                    // Check if the prefab is already instantiated
                    if (changeablePlant.transform.Find(prefabVariant.name) == null)
                    {
                        Vector3 childPosition = child.position;
                        Quaternion childRotation = child.rotation;
                        GameObject newChild = Instantiate(prefabVariant, childPosition, childRotation, changeablePlant.transform);
                        //Debug.Log($"Replaced child with prefab variant {prefabVariant.name}");
                    }
                    else
                    {
                        //Debug.LogWarning($"Prefab {prefabVariant.name} already exists. Skipping instantiation.");
                    }
                    return;
                }
                
            }
                Debug.LogWarning($"No child found to replace under {changeablePlant.name}");
        }
        else
        {
            Debug.LogWarning($"Parent plantID {this.plantID} does not match targetPlantID {targetPlantID}");
        }
    }
}
