using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class NPC : MonoBehaviour
{
    [SerializeField] private SpriteRenderer interactSprite;

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

    private float interactDistance = 5f;

    private bool introPlayed = false;
    private bool midpointTransitionPlayed = false;
    private bool secondTransitionPlayed = false;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object in the scene
    }

    private void Update()
    {
        if (!DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Check if the 'E' key is pressed
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                // Interact
                Interact(); // Call the interact method
            }
        }

        //  If the player is not within distance
        if (interactSprite.gameObject.activeSelf && !IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(false); // Deactivate the interact sprite
        }

        // Else  if the player is within distance...
        else if (!interactSprite.gameObject.activeSelf && IsWithinInteractDistance())
        {
            interactSprite.gameObject.SetActive(true); // Activate the interact sprite
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
        }
        
        if (WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(AfterFirstWaterTest);
        }

        if (!midpointTransitionPlayed && WaterTestingManager.isFirstWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(MidpointTransition);
            Invoke("SetMidpointTransitionPlayed", 0.5f); // Call the SetMidpointTransitionPlayed method after 0.5 seconds
        }

        if (midpointTransitionPlayed && !TrashCollectionGame.trashCollected && !PlantGameManager.plantingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(TrashCollectionTutorial); 
        }

        if (TrashCollectionGame.trashCollected && !PlantGameManager.plantingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(PlantingTutorial);
        }

        if (PlantGameManager.plantingCompleted && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(RetestWaterTutorial);
        }

        if (!secondTransitionPlayed && WaterTestingManager.isSecondWaterTestComplete && !AnimalGameManager.trappingCompleted)
        {
            DialogueManager.GetInstance().StartDialogue(AfterSecondWaterTest);
            Invoke("SecondTransition", 0.5f); // Call the SecondTestComplete method after 0.5 seconds
        }

        if (!AnimalGameManager.trappingCompleted && secondTransitionPlayed)
        {
            DialogueManager.GetInstance().StartDialogue(AnimalTrappingTutorial);
        }

        if (AnimalGameManager.trappingCompleted)
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
