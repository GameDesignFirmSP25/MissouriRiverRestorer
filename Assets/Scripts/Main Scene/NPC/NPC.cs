using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

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
     // [SerializeField] private TextAsset Finale;

     [SerializeField]
     private Transform playerTransform;

     private float interactDistance = 8f;

     private bool isWithinInteractDistance = false;

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
               // Model axis not aligned with Unity. Add 90 to y rotation to compensate
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
                    DialogueManager.GetInstance().StartDialogue(TutorialIntroduction); // Call the StartDialogue method from the DialogueManager class
                    if (!firstWaterTasked)
                    {
                         actionNames["FirstWaterGameTasked"]?.Invoke();
                         firstWaterTasked = true;
                    }
                    break;
               // Trash game task
               case 2:
                    DialogueManager.GetInstance().StartDialogue(AfterFirstWaterTest);
                    if (!trashTasked)
                    {
                         actionNames["TrashGameTasked"]?.Invoke();
                         trashTasked = true;
                    }
                    break;
               // Animal game task
               case 4:
                    DialogueManager.GetInstance().StartDialogue(AfterTrashCollection);
                    if (!animalTasked)
                    {
                         actionNames["PlantAndAnimalGameTasked"]?.Invoke();
                         animalTasked = true;
                    }
                    break;
               // Second water task
               case 6:
                    DialogueManager.GetInstance().StartDialogue(RetestWaterTutorial);
                    if (!secondWaterTasked)
                    {
                         actionNames["SecondWaterGameTasked"]?.Invoke();
                         secondWaterTasked = true;
                    }
                    break;
               // Finale
               case 8:
                    DialogueManager.GetInstance().StartDialogue(AfterSecondWaterTest);
                    if (!finalEvent)
                    {
                         actionNames["FinalDialogue"]?.Invoke();
                         finalEvent = true;
                    }
                    break;
          }
     }

     private bool IsWithinInteractDistance()
     {
          return (Vector3.Distance(playerTransform.position, transform.position) < interactDistance); // Check if the player is within a distance of 2 units from the NPC
     }
}
