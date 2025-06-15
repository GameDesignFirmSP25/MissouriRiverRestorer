using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class InteractableObject : MonoBehaviour
{
    [Header("GameObject References")]
    public GameObject InteractPopup;

    [Header("Scriptable Object Reference")]
    public ObjectSO ObjectSO;

    [Header("Booleans")]
    public bool isWithinRange = false;
    public bool hasBeenInteractedWith = false;

    [Header("Data")]
    public ObjectSO data;

    [Header("UI Elements")]
    public GameObject interactPanel; // Reference to the UI Panel for interaction
    public Image interactPanelImage; // Reference to the UI image for interaction prompt
    public TextMeshProUGUI interactPanelText; // Reference to the UI text for interaction prompt

    [Header("Lists")]
    public static List<InteractableObject> allGBInteractableObjects = new List<InteractableObject>();

    [Header("Materials")]
    private Material animalInteraction;
    private Material plantInteraction;

    void Awake()
    {
        if (InteractPopup != null)
            InteractPopup.SetActive(false);
    }

    private void Start()
    {
        interactPanel = GameObject.Find("Interact Panel");

        interactPanelImage = interactPanel.GetComponent<Image>();

        interactPanelText = interactPanel.GetComponentInChildren<TextMeshProUGUI>();

        interactPanelImage.enabled = false;

        interactPanelText.text = "";

        GameObject targetChild = GameObject.FindWithTag("Guidebook Objects");
        Renderer renderer = null;

        if (targetChild != null)
        {
            renderer = targetChild.GetComponentInChildren<Renderer>();
        }

        if (renderer != null)
        {
            animalInteraction = new Material(renderer.material);
            plantInteraction = new Material(renderer.material);

            animalInteraction.SetFloat("_OutlineType", 3);
            plantInteraction.SetFloat("_OutlineType", 5);

            if (data.Name == "Asian Carp" || data.Name == "Bald Eagle" || data.Name == "Banded Pennant Dragonfly" || data.Name == "Beaver" ||
                data.Name == "Common Garter Snake" || data.Name == "European Starling" || data.Name == "Muskrat" || data.Name == "Northern Map Turtle" ||
                data.Name == "Painted Lady Butterfly" || data.Name == "Pallid Sturgeon" || data.Name == "Raccoon" || data.Name == "Snapping Turtle" ||
                data.Name == "White-Tailed Deer")
            {
                renderer.material = animalInteraction;
            }

            if (data.Name == "American Lotus" || data.Name == "Box Elder" || data.Name == "Bradford Pear"|| data.Name == "Cordgrass" ||
                data.Name == "Purple Loosestrife" || data.Name == "Swamp Milkweed" ||data.Name == "Sycamore" || data.Name == "Yellow Coneflower")
            {
                renderer.material = plantInteraction;
            }
        }
        else
        {
            Debug.LogWarning("Renderer not found on the interactable object.");
        }
    }

    private void Update()
    {
        if (isWithinRange)
        {
            InteractPopup.transform.LookAt(Camera.main.gameObject.transform);
        }

        foreach (var obj in allGBInteractableObjects)
        {
            if (obj.ObjectSO != null && obj.ObjectSO.isScanned)
            {
                obj.hasBeenInteractedWith = true;
            }
        }
    }

    public void InRange()
    {
        isWithinRange = true;
        if (InteractPopup != null)
            InteractPopup.SetActive(true);
        ShowInteractionUI();
    }

    public void OutRange()
    {
        isWithinRange = false;
        if (InteractPopup != null)
            InteractPopup.SetActive(false);
        HideInteractionUI();
    }

    private void ShowInteractionUI()
    {
        interactPanelImage.enabled = true;
        interactPanelText.text = "E to Interact with " + data.Name;

        if (data.Name == "Asian Carp" || data.Name == "Bald Eagle" || data.Name == "Banded Pennant Dragonfly" || data.Name == "Beaver" ||
                data.Name == "Common Garter Snake" || data.Name == "European Starling" || data.Name == "Muskrat" || data.Name == "Northern Map Turtle" ||
                data.Name == "Painted Lady Butterfly" || data.Name == "Pallid Sturgeon" || data.Name == "Raccoon" || data.Name == "Snapping Turtle" ||
                data.Name == "White-Tailed Deer")
        {
            animalInteraction.SetFloat("_HasOutline", 1.0f);
        }

        if (data.Name == "American Lotus" || data.Name == "Box Elder" || data.Name == "Bradford Pear" || data.Name == "Cordgrass" ||
                data.Name == "Purple Loosestrife" || data.Name == "Swamp Milkweed" || data.Name == "Sycamore" || data.Name == "Yellow Coneflower")
        {
            plantInteraction.SetFloat("_HasOutline", 1.0f);
        }
    }

    public void HideInteractionUI()
    {
        interactPanelImage.enabled = false;
        interactPanelText.text = "";

        if (data.Name == "Asian Carp" || data.Name == "Bald Eagle" || data.Name == "Banded Pennant Dragonfly" || data.Name == "Beaver" ||
                data.Name == "Common Garter Snake" || data.Name == "European Starling" || data.Name == "Muskrat" || data.Name == "Northern Map Turtle" ||
                data.Name == "Painted Lady Butterfly" || data.Name == "Pallid Sturgeon" || data.Name == "Raccoon" || data.Name == "Snapping Turtle" ||
                data.Name == "White-Tailed Deer")
        {
            animalInteraction.SetFloat("_HasOutline", 0.0f);
        }

        if (data.Name == "American Lotus" || data.Name == "Box Elder" || data.Name == "Bradford Pear" || data.Name == "Cordgrass" ||
                data.Name == "Purple Loosestrife" || data.Name == "Swamp Milkweed" || data.Name == "Sycamore" || data.Name == "Yellow Coneflower")
        {
            plantInteraction.SetFloat("_HasOutline", 0.0f);
        }
    }

    private void OnEnable()
    {
        allGBInteractableObjects.Add(this);
    }

    private void OnDisable()
    {
        allGBInteractableObjects.Remove(this);
    }

    public void SetObjectInteactedWith()
    {
        hasBeenInteractedWith = true;
    }
}
