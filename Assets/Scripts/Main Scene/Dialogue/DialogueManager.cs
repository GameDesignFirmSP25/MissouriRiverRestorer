using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;


public class DialogueManager : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Dialogue Box")]
    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private TextMeshProUGUI dialogueText;

    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;

    [Header("Input Settings")]
    [SerializeField] private KeyCode skipKey = KeyCode.Space;

    [Header("Booleans")]
    [SerializeField] private bool canSkip = true;
    public bool dialogueIsPlaying { get; private set; }
    private bool canContinueToNextLine = false;

    [Header("UI Elements")]
    private TextMeshProUGUI[] choicesText;

    [Header("References")]
    private Story currentStory;
    private Coroutine displayLineCoroutine;
    private static DialogueManager instance;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager exists
        if (instance == null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene"); // Debug.Log a warning if more than one Dialogue Manager is found
        }

        instance = this; // Set the instance to this DialogueManager instance
    }

    // Returns the instance of DialogueManager
    public static DialogueManager GetInstance()
    {
        return instance; // Return the instance of DialogueManager
    }

    private void Start()
    {
        dialogueIsPlaying = false; // Set bool dialogueIsPlaying to false at the start
        dialogueBox.SetActive(false); // Hide the dialogue box at the start

        choicesText = new TextMeshProUGUI[choices.Length]; // Get the choices from the story
        int index = 0; // Initialize index to 0

        // Loop through the choices and get the TextMeshProUGUI component from each choice
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>(); // Get the TextMeshProUGUI component from each choice
            index++; // Increment index
        }

        StartCoroutine(SelectFirstChoice()); // Call the method to select the first choice
    }

    private void Update()
    {
        // If bool dialogueIsPlaying is false...
        if (!dialogueIsPlaying)
        {
            return; // Exit the Update method
        }

        // Handle input for continuing the story
        // If 
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && (Input.GetKeyDown(KeyCode.E)))
        {
            ContinueStory(); // Call the method to continue the story
        }
    }

    // Method to start the dialogue with a given ink JSON file
    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text); // Create a new Story object with the provided ink JSON
        dialogueIsPlaying = true; // Set bool dialogueIsPlaying to true
        dialogueBox.SetActive(true); // Show the dialogue box
        ContinueStory(); // Call the method to continue the story
    }

    // Method to end the dialogue
    private void EndDialogue()
    {
        dialogueIsPlaying = false; // Set bool dialogueIsPlaying to false
        dialogueBox.SetActive(false); // Hide the dialogue box
        dialogueText.text = ""; // Clear the dialogue text
    }

    //Method to continue the story
    private void ContinueStory()
    {
        // If currentStory can continue...
        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine); // Stop the current coroutine if it is running
            }
            string nextLine = currentStory.Continue(); // Get the next line of dialogue from the story

            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                EndDialogue(); // If there is no more dialogue, end the dialogue
            }
            else
            {
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine)); // Start the coroutine to display the line
            }
        }
        else
        {
            EndDialogue(); // If there is no dialogue, end the dialogue
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        dialogueText.text = line; // Set the dialogue text to the current line
        dialogueText.maxVisibleCharacters = 0; // Reset the max visible characters to 0 to start the typing animation 
        HideChoices(); // Hide the choices UI elements
        canContinueToNextLine = false; // Set bool canContinueToNextLine to false
        bool isAddingRichTextTag = false; // Set bool isAddingRichTextTag to false

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // If bool canSkiop is true and the skip key is pressed...
            if (canSkip && Input.GetKeyDown(skipKey))
            {
                Debug.Log("Typing animation skipped."); // Log the skip action
                dialogueText.maxVisibleCharacters = line.Length; // Display the full line
                break; // Exit the loop
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true; //set bool isAddingRichTextTag to true

                // If the letter is a closing tag, set isAddingRichTextTag to false
                if (letter == '>')
                {
                    isAddingRichTextTag = false; // Set bool isAddingRichTextTag to false   
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.maxVisibleCharacters++; // Add the next letter
                yield return new WaitForSeconds(typingSpeed); // Wait for a small time
            }
        }

        // actions to take after the entire line has finished displaying
        DisplayChoices();

        canContinueToNextLine = true; // Set bool canContinueToNextLine to true
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices; // Get the current choices from the story

        int index = 0; // Initialize index to 0

        // Enable and initalize the choices
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true); // Activate the choice text
            choicesText[index].text = choice.text; // Set the choice text
            index++; // Increment index
        }

        // Go through the remaining choices and disable them
        for (int i = index; i < choicesText.Length; i++)
        {
            choices[i].gameObject.SetActive(false); // Deactivate the choice text
        }
    }

    private void HideChoices()
    {
        // For each choice in the choices array...
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false); // Deactivate the choice text
        }
    }
    // Method to select the first choice in the choices array
    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait for at least a frame before we set the current selected GameObject
        EventSystem.current.SetSelectedGameObject(null); // Deselect any currently selected GameObject
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject); // Select the first choice
    }

    // Method to make a choice based on the index of the choice
    public void MakeChoice(int choiceIndex)
    {
        // If canContinueToNextLine is true...
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex); // Choose the choice based on the index
            
            Input.GetKeyDown(KeyCode.Space); // Simulate key press
            ContinueStory(); // Continue the story after making the choice
        }
    }
}
