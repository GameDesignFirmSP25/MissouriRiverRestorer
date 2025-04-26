using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChangeablePlant : MonoBehaviour
{
    public bool isSwapped = false;

    public GameObject[] originalPlant;
    public GameObject[] swappedPlant;
    //private GameObject currentPlant;
    //public GameObject plantChild;
    public GameObject changeablePlant;
    public string childName1 = "Bradford Pear Tree";
    public string childName2 = "Invasive Plant (lower bank)";
    public string childName3 = "Invasive Plant (mid bank)";
    public string childName4 = "Invasive Plant (upper bank)";
    public string plantID;

    public AnimalGameManager animalGameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Initializing ChangeablePlant for {gameObject.name}");
        for (int i = 0; i < originalPlant.Length; i++)
        {
            Debug.Log($"originalPlant[{i}] = {originalPlant[i]?.name ?? "null"}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetOriginalPlant()
    {
        GameObject plant1 = originalPlant[0]; // Get the Breadford Pear Tree
        GameObject plant2 = originalPlant[1];
        GameObject plant3 = originalPlant[2];
        GameObject plant4 = originalPlant[3];
    }

    private void GetSwappedPlant()
    {
        GameObject plant5 = swappedPlant[0]; // Get the Sycamore Tree
        GameObject plant6 = swappedPlant[1]; // Get the American Lotus Flower
        GameObject plant7 = swappedPlant[2]; // Get the Gordgrass
        GameObject plant8 = swappedPlant[3]; // Get the Swamp Milkweed
        GameObject plant9 = swappedPlant[4]; // Get the Yellow Coneflower
    }

    public void SwapPlants()
    {
        // Pop up panel

        // If plant is Bradford Pear Tree, swap with Sycamore Tree
        // Sycamore becomes plantParent


        // If plant is invasive plant from originalPlant array, choose native plant from swappedPlant array. 
        // Chosen plant becomes plantParent

        // Ensure the plant is not already swapped
        if (isSwapped)
        {
            Debug.LogWarning($"Plant {plantID} has already been swapped.");
            return;
        }

        // Ensure the original and swapped plants are properly assigned
        if (originalPlant.Length == 0 || swappedPlant.Length == 0)
        {
            Debug.LogError($"Plant {plantID} does not have valid original or swapped plants assigned.");
            return;
        }

        if (!isSwapped)
        {
            // Find the index of the clicked object in the originalPlant array
            int originalPlantIndex = System.Array.IndexOf(originalPlant, gameObject);

            if (originalPlantIndex >= 0) // If the object is found in the array
            {
                Debug.Log($"Clicked on plant at index {originalPlantIndex}");

                // Perform an action based on the index
                if (originalPlantIndex == 0) // Example: Bradford Pear Tree
                {
                    Transform childTransform = changeablePlant.transform.Find(childName1);
                    
                    Debug.Log("Destroying Bradford Pear Tree");
                    Destroy(childTransform.gameObject);
                    Debug.Log("Swapping Plants...");
                    isSwapped = true; // Set the plant as swapped
                    ExecuteSpawnOfSycamore();
                    
                }
                else if (originalPlantIndex == 1) // Example: Another plant
                {
                    Transform childTransform = changeablePlant.transform.Find(childName1);

                    Debug.Log("Destroying Bradford Pear Tree");
                    Destroy(childTransform.gameObject);
                    Debug.Log("Swapping Plants...");
                    isSwapped = true; // Set the plant as swapped
                    ExecuteSpawnOfSycamore();
                }
                else if (originalPlantIndex == 2)
                {
                    Transform childTransform = changeablePlant.transform.Find(childName1);

                    Debug.Log("Destroying Bradford Pear Tree");
                    Destroy(childTransform.gameObject);
                    Debug.Log("Swapping Plants...");
                    isSwapped = true; // Set the plant as swapped
                    ExecuteSpawnOfSycamore();
                }
            }
            else
            {
                Debug.Log("Clicked object is not in the originalPlant array.");
            }
        }
        
    }

    public void ExecuteSpawnOfSycamore()
    {
        if (changeablePlant == null)
        {
            Debug.LogError("changeablePlant is null! Cannot spawn Sycamore Tree.");
            return;
        }

        Debug.Log("Swapping Bradford Pear Tree with Sycamore Tree.");
        InstantiatePrefabAsChild(changeablePlant, swappedPlant[0]);
    }

    public void InstantiatePrefabAsChild(GameObject parentObject, GameObject prefab)
    {
        // Instantiate the prefab
        GameObject instantiatedPrefab = Instantiate(prefab);

        // Set the parent of the instantiated prefab
        instantiatedPrefab.transform.SetParent(parentObject.transform);

        //// Optionally reset the local position, rotation, and scale
        instantiatedPrefab.transform.localPosition = Vector3.zero;
        instantiatedPrefab.transform.localRotation = Quaternion.identity;
        isSwapped = false;

        Debug.Log($"Prefab instantiated as a child of {parentObject.name}");
    }
}
