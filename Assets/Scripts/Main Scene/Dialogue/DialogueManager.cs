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

    private TextMeshProUGUI[] choicesText;

    private Story currentStory;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false; 

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of DialogueManager exists
        if (instance == null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene"); // Debug.Log a warning if more than one Dialogue Manager is found
        }

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
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
        if (canContinueToNextLine && currentStory.currentChoices.Count == 0 && (Input.GetKeyDown(KeyCode.Space)))
        {
            ContinueStory();
        }
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text); // Create a new Story object with the provided ink JSON
        dialogueIsPlaying = true; // Set bool dialogueIsPlaying to true
        dialogueBox.SetActive(true); // Show the dialogue box

        ContinueStory(); // Call the method to continue the story
    }

    private void EndDialogue()
    {
        dialogueIsPlaying = false; // Set bool dialogueIsPlaying to false
        dialogueBox.SetActive(false); // Hide the dialogue box
        dialogueText.text = ""; // Clear the dialogue text
    }

    private void ContinueStory()
    {
        //// If currentStory can continue...
        //if (currentStory.canContinue)
        //{
        //    dialogueText.text = currentStory.Continue(); // Display the first line of dialogue
        //    DisplayChoices(); // Display choices, if any, for this dialogue line
        //}
        //else
        //{
        //    EndDialogue(); // If there is no dialogue, end the dialogue
        //}

        if (currentStory.canContinue)
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            string nextLine = currentStory.Continue();

            // handle case where the last line is an external function
            if (nextLine.Equals("") && !currentStory.canContinue)
            {
                EndDialogue();
            }
            else
            {
                displayLineCoroutine = StartCoroutine(DisplayLine(nextLine));
            }
        }
        else
        {
            EndDialogue(); // If there is no dialogue, end the dialogue
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag)
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        DisplayChoices();

        canContinueToNextLine = true;
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
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false); // Deactivate the choice text
        }
    }

    private IEnumerator SelectFirstChoice()
    {
        // Event System requires we clear it first, then wait for at least a frame before we set the current selected GameObject
        EventSystem.current.SetSelectedGameObject(null); // Deselect any currently selected GameObject
        yield return new WaitForEndOfFrame(); // Wait for the end of the frame
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject); // Select the first choice
    }

    public void MakeChoice(int choiceIndex)
    {

        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex); // Choose the choice based on the index
            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            Input.GetKeyDown(KeyCode.Space); // this is specific to my InputManager script
            ContinueStory();
        }
    }
}
