using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class NPC : MonoBehaviour
{
    [SerializeField] private SpriteRenderer interactSprite;
    [SerializeField] private GameObject interactText; // Reference to the interact text object
    [SerializeField] private Transform player; // Reference to the player object

    [Header("Ink JSON")]
    [SerializeField] private TextAsset TutorialIntroduction;
    //[SerializeField] private TextAsset WaterTestingTutorial;
    [SerializeField] private TextAsset AfterFirstWaterTest;
    // [SerializeField] private TextAsset MidpointTransition;
    //[SerializeField] private TextAsset TrashCollectionTutorial;
    [SerializeField] private TextAsset AfterTrashCollection;
    // [SerializeField] private TextAsset PlantingTutorial;
    // [SerializeField] private TextAsset AnimalTrappingTutorial;
    [SerializeField] private TextAsset RetestWaterTutorial;
    [SerializeField] private TextAsset AfterSecondWaterTest;
    // [SerializeField] private TextAsset Finale;

    private Transform playerTransform;

    private float interactDistance = 8f;

    private bool isWithinInteractDistance = false;
    private static bool introPlayed = false;
    private static bool midpointTransitionPlayed = false;
    private static bool firstTransitionPlayed = false;
    private static bool secondTransitionPlayed = false;
    private static bool thirdTransitionPlayed = false; 

    // Methods to trigger game progression.
     public void OnFirstWaterGameTasked() { Debug.Log("NPC: First Water Game Tasked!"); }
     public void OnTrashGameTasked() { Debug.Log("NPC: Trash Game Tasked!"); }

     public void OnSecondWaterGameTasked() { Debug.Log("NPC: Second Water Game Tasked!"); }
     public void OnPlantAndAnimalGameTasked() { Debug.Log("NPC: Animal Game Tasked!"); }
     public void OnFinalDialogue() { Debug.Log("NPC: Final Dialogue!"); }

     // Dictionary to match names to event complete methods
     public Dictionary<string, UnityAction> actionNames;
     private void Awake()
     {
          actionNames = new Dictionary<string, UnityAction>
          {
               {"FirstWaterGameTasked",  OnFirstWaterGameTasked},
               {"TrashGameTasked",  OnTrashGameTasked},
               {"SecondWaterGameTasked",  OnSecondWaterGameTasked},
               {"PlantAndAnimalGameTasked",  OnPlantAndAnimalGameTasked},
               {"FinalDialogue",  OnFinalDialogue},
          };
     }

     private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player object in the scene
     }

     private void Update()
    {
        // Smoothly rotate the NPC to look at the player
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Adjust the speed (5f) as needed
        }

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
            actionNames["FirstWaterGameTasked"]?.Invoke();
        }

         if (introPlayed && !firstTransitionPlayed && WaterTestingManager.isFirstWaterTestComplete && !WaterTestingManager.isSecondWaterTestComplete)
         {
            DialogueManager.GetInstance().StartDialogue(AfterFirstWaterTest);
            Invoke("SetFirstTransitionPlayed", 0.5f); // Call the SetFirstTransitionPlayed method after 0.5 seconds
            Invoke("SetMidpointTransitionPlayed", 0.5f);
            actionNames["TrashGameTasked"]?.Invoke();
         }

        if (midpointTransitionPlayed && TrashCollectionGame.trashCollected && !PlantGameManager.plantingCompleted && !secondTransitionPlayed)
        {
            DialogueManager.GetInstance().StartDialogue(AfterTrashCollection);
            Invoke("SecondTransition", 0.5f); // Call the SecondTransition method after 0.5 seconds
            actionNames["PlantAndAnimalGameTasked"]?.Invoke();
        }

        if (TrashCollectionGame.trashCollected && AnimalGameManager.trappingCompleted && !WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(RetestWaterTutorial);
            actionNames["SecondWaterGameTasked"]?.Invoke();
        }

        if (!thirdTransitionPlayed && WaterTestingManager.isSecondWaterTestComplete)
        {
            DialogueManager.GetInstance().StartDialogue(AfterSecondWaterTest);
            Invoke("SecondTransition", 0.5f); // Call the SecondTestComplete method after 0.5 seconds
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

    private void ThirdTransition()
    {
        thirdTransitionPlayed = true; // Set thirdTransitionPlayed to true
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
