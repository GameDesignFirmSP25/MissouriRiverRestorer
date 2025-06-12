using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEditor;

public class NPC : MonoBehaviour
{
     [SerializeField] private SpriteRenderer interactSprite;
     [SerializeField] private GameObject interactText; // Reference to the interact text object

     [Header("Ink JSON")]
     [SerializeField] private TextAsset TutorialIntroduction;
     [SerializeField] private TextAsset AfterFirstWaterTest;
     [SerializeField] private TextAsset AfterTrashCollection;
     [SerializeField] private TextAsset RetestWaterTutorial;
     [SerializeField] private TextAsset AfterSecondWaterTest;
     [SerializeField] private TextAsset RepeatObjectiveForWaterTesting;
     [SerializeField] private TextAsset RepeatObjectiveForTashCollection;
     [SerializeField] private TextAsset RepeatObjectiveForConservation;
     [SerializeField] private TextAsset RepeatObjectiveForQuiz;
    // [SerializeField] private TextAsset Finale;

    [SerializeField]
     private Transform playerTransform;

     private float interactDistance = 8f;

     private bool isWithinInteractDistance = false;
     public static bool isInteractable = true;

     // Methods to trigger game progression.
     private bool firstWaterTasked = false;
     private bool trashTasked = false;
     private bool animalTasked = false;
     private bool secondWaterTasked = false;
     private bool finalEvent = false;

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

     private void Update()
     {
          // Smoothly rotate the NPC to look at the player
          if (playerTransform != null)
          {
               Vector3 direction = (playerTransform.position - transform.position).normalized;
               Quaternion lookRotation = Quaternion.LookRotation(direction);
               // Model axis not aligned with Unity. 
               lookRotation.eulerAngles = new Vector3(0f, lookRotation.eulerAngles.y, 0f);
               transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Adjust the speed (5f) as needed
          }

          if (!DialogueManager.GetInstance().dialogueIsPlaying && isWithinInteractDistance && Input.GetKeyDown(KeyCode.E))
          {
               Interact(); // Call the interact method
          }

          //  If the player is not within distance
          if (interactText.gameObject.activeSelf && !IsWithinInteractDistance())
          {
               //interactSprite.gameObject.SetActive(false); // Deactivate the interact sprite
               interactText.SetActive(false); // Deactivate the interact text
               isWithinInteractDistance = false; // set bool isWithinInteractDistance to false
          }

          // Else  if the player is within distance...
          else if (!interactText.gameObject.activeSelf && IsWithinInteractDistance())
          {
               //interactSprite.gameObject.SetActive(true); // Activate the interact sprite
               interactText.SetActive(true); // Activate the interact text
               isWithinInteractDistance = true; // set bool isWithinInteractDistance to true
          }
     }

     private void Interact()
     {
          if(GameProgressManager.instance.isAllEventsCompleted)
          {
               // TODO: Add end game one-liner
               return;
          }
          switch (GameProgressManager.instance.CurrentProgressionStep)
          {
               // Intro and first water task
               case 0:
                    
                    if (!firstWaterTasked && isInteractable)
                    {
                         DialogueManager.GetInstance().StartDialogue(TutorialIntroduction); // Call the StartDialogue method from the DialogueManager class
                         actionNames["FirstWaterGameTasked"]?.Invoke();
                         firstWaterTasked = true;
                         isInteractable = false;
                         
                    }
                    break;
               // Repeat objective for first water task
               case 1:

                    if (firstWaterTasked && isInteractable)
                    {
                         Debug.Log("NPC: First water task already completed. Repeating objective for water testing.");
                         DialogueManager.GetInstance().StartDialogue(RepeatObjectiveForWaterTesting);
                         isInteractable = false; // Prevent further interaction until the next task
                    }
                    break;
               // Trash game task
               case 2:
                    
                    if (!trashTasked && isInteractable)
                    {
                         DialogueManager.GetInstance().StartDialogue(AfterFirstWaterTest);
                         actionNames["TrashGameTasked"]?.Invoke();
                         trashTasked = true;
                         isInteractable = false;
                    }
                    break;
               // Repeat objective for trash task
               case 3:

                    if (trashTasked && isInteractable)
                    {
                         Debug.Log("NPC: Trash task already completed. Repeating objective for trash collection.");
                         DialogueManager.GetInstance().StartDialogue(RepeatObjectiveForTashCollection);
                         isInteractable = false; // Prevent further interaction until the next task
                    }
                    break; // Add break to prevent fall-through
               // Animal game task
               case 4:
                    
                    if (!animalTasked && isInteractable)
                    {
                         DialogueManager.GetInstance().StartDialogue(AfterTrashCollection);
                         actionNames["PlantAndAnimalGameTasked"]?.Invoke();
                         animalTasked = true;
                         isInteractable = false;
                    }
                    break;
               // Repeat objective for animal task
               case 5:

                    if (animalTasked && isInteractable)
                    {
                         Debug.Log("NPC: Animal task already completed. Repeating objective for conservation.");
                         DialogueManager.GetInstance().StartDialogue(RepeatObjectiveForConservation);
                         isInteractable = false; // Prevent further interaction until the next task
                    }   
                    break;
               // Second water task
               case 6:
                    
                    if (!secondWaterTasked && isInteractable)
                    {
                         DialogueManager.GetInstance().StartDialogue(RetestWaterTutorial);
                         actionNames["SecondWaterGameTasked"]?.Invoke();
                         secondWaterTasked = true;
                         isInteractable = false;
                    }
                    break;
               // Repeat objective for second water task
               case 7:

                    if (secondWaterTasked && isInteractable)
                    {
                         Debug.Log("NPC: Second water task already completed. Repeating objective for water testing.");
                         DialogueManager.GetInstance().StartDialogue(RepeatObjectiveForWaterTesting);
                         isInteractable = false; // Prevent further interaction until the next task
                    }
                break;
               // Finale
               case 8:

                    if (!finalEvent && isInteractable)
                    {
                         DialogueManager.GetInstance().StartDialogue(AfterSecondWaterTest);
                         actionNames["FinalDialogue"]?.Invoke();
                         finalEvent = true;
                         isInteractable = false;
                    }
                    break;
               // Repeat objective for final task
               case 9:

                    if (finalEvent && isInteractable)
                    {
                         Debug.Log("NPC: Final task already completed. Repeating objective for quiz.");
                         DialogueManager.GetInstance().StartDialogue(RepeatObjectiveForQuiz);
                         isInteractable = false; // Prevent further interaction until the next task
                    }
                    break;
          }
     }

     private bool IsWithinInteractDistance()
     {
          return (Vector3.Distance(playerTransform.position, transform.position) < interactDistance); // Check if the player is within a distance of 2 units from the NPC
     }
}
