using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [SerializeField] private SpriteRenderer interactSprite;
    [SerializeField] private GameObject interactText; // Reference to the interact text object

    [Header("Ink JSON")]
    [SerializeField] private TextAsset TutorialIntroduction;
    [SerializeField] private TextAsset WaterTestingTutorial;
    [SerializeField] private TextAsset AfterFirstWaterTest;
    [SerializeField] private TextAsset MidpointTransition;
    [SerializeField] private TextAsset TrashCollectionTutorial;
    [SerializeField] private TextAsset PlantingTutorial;
    [SerializeField] private TextAsset RetestWaterTutorial;
    [SerializeField] private TextAsset AfterSecondWaterTest;
    [SerializeField] private TextAsset AnimalTrappingTutorial;
    [SerializeField] private TextAsset Finale;

    private Transform playerTransform;

    private float interactDistance = 8f;

    private bool isWithinInteractDistance = false;
    private static bool introPlayed = false;
    private static bool midpointTransitionPlayed = false;
    private static bool firstTransitionPlayed = false;
    private static bool secondTransitionPlayed = false;

     // Events to trigger game progression.
     public UnityAction FirstWaterGameTasked;
     public UnityAction TrashGameTasked;
     public UnityAction PlantGameTasked;
     public UnityAction SecondWaterGameTasked;
     public UnityAction AnimalGameTasked;


     private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object in the scene
    }

    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            if (isWithinInteractDistance) // Check if the player is within a distance of 2 units from the NPC
            {
                // Check if the 'E' key is pressed
                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    // Interact
                    Interact(); // Call the interact method
                }
            } 
        }

        //  If the player is not within distance
        if (interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(false); // Deactivate the interact sprite
            interactText.SetActive(false); // Deactivate the interact text
            isWithinInteractDistance = false; // set bool isWithinInteractDistance to false
        }

        // Else  if the player is within distance...
        else if (!interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(true); // Activate the interact sprite
            interactText.SetActive(true); // Activate the interact text
            isWithinInteractDistance = true; // set bool isWithinInteractDistance to true
        }
    }

    private void Interact()
    {
        if (!introPlayed)
        {
            DialogueManager.GetInstance().StartDialogue(TutorialIntroduction); // Call the StartDialogue method from the DialogueManager class
            Invoke("SetIntroPlayed", 0.5f); // Call the SetIntroPlayed method after 0.5 seconds
        }

        if (introPlayed && !WaterTestingManager.isFirstWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(WaterTestingTutorial);
               FirstWaterGameTasked.Invoke();
        }
        
        if (introPlayed && !firstTransitionPlayed && WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(AfterFirstWaterTest);
            Invoke("SetFirstTransitionPlayed", 0.5f); // Call the SetFirstTransitionPlayed method after 0.5 seconds
        }

        if (firstTransitionPlayed && !midpointTransitionPlayed && WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(MidpointTransition);
            Invoke("SetMidpointTransitionPlayed", 0.5f); // Call the SetMidpointTransitionPlayed method after 0.5 seconds
        }

        if (midpointTransitionPlayed && !TrashCollectionGame.trashCollected && !PlantGameManager.plantingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(TrashCollectionTutorial); 
               TrashGameTasked.Invoke();
        }

        if (midpointTransitionPlayed && TrashCollectionGame.trashCollected && !PlantGameManager.plantingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(PlantingTutorial);
               PlantGameTasked.Invoke();
        }

        if (TrashCollectionGame.trashCollected && PlantGameManager.plantingCompleted && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(RetestWaterTutorial);
               SecondWaterGameTasked.Invoke();
        }

        if (!secondTransitionPlayed && WaterTestingManager.isSecondWaterTestComplete && !AnimalGameManager.trappingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(AfterSecondWaterTest);
            Invoke("SecondTransition", 0.5f); // Call the SecondTestComplete method after 0.5 seconds
        }

        if (!AnimalGameManager.trappingCompleted && secondTransitionPlayed)
        {
            DialogueManager.GetInstance().StartDialogue(AnimalTrappingTutorial);
               AnimalGameTasked.Invoke();
        }

        if (TrashCollectionGame.trashCollected && AnimalGameManager.trappingCompleted && PlantGameManager.plantingCompleted && WaterTestingManager.isFirstWaterTestComplete && WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(Finale); // Call the StartDialogue method from the DialogueManager class
        }

    }

    private void SetIntroPlayed()
    {
        introPlayed = true; // Set introPlayed to true
    }

    private void SetMidpointTransitionPlayed()
    {
        midpointTransitionPlayed = true; // Set midpointTransitionPlayed to true
    }

    private void SetFirstTransitionPlayed()
    {
        firstTransitionPlayed = true; // Set firstTransitionPlayed to true
    }

    private void SecondTransition()
    {
        secondTransitionPlayed = true; // Set secondTransitionPlayed to true
    }

    private bool IsWithinInteractDistance()
    {
        if (Vector3.Distance(playerTransform.position, transform.position) < interactDistance) // Check if the player is within a distance of 2 units from the NPC
        {
            return true; // Return true if within distance
        }
        else
        {
            return false; // Return false if not within distance
        }
    }
}
