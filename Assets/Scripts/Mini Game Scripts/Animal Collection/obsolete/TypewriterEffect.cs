using System;
using System.Collections;
using UnityEngine;
using TMPro;
using Object = UnityEngine.Object;

public class TypewriterEffect : MonoBehaviour
{
    private TMP_Text _textBox;

    [Header("Basic Typewriter Functionality")]
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;


    [Header("Skipping Functionality")]
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;


    [Header("Event Functionality")]
    private WaitForSeconds _textboxFullEventDelay;
    [SerializeField][Range(0.1f, 0.5f)] private float sendDoneDelay = 0.25f;

    public static event Action CompleteTextRevealed;
    public static event Action<char> CharacterRevealed;


    private void Awake()
    {
        _textBox = GetComponent<TMP_Text>();

        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);

        _skipDelay = new WaitForSeconds(1 / (charactersPerSecond * skipSpeedup));
        _textboxFullEventDelay = new WaitForSeconds(sendDoneDelay);
    }

    //private void OnEnable()
    //{
    //    TMPro_EventManager.TEXT_CHANGED_EVENT.Add(PrepareForNewText); // Subscribe to the TEXT_CHANGED_EVENT to prepare for new text
    //}

    //private void OnDisable()
    //{
    //    TMPro_EventManager.TEXT_CHANGED_EVENT.Remove(PrepareForNewText); // Unsubscribe from the TEXT_CHANGED_EVENT to avoid memory leaks
    //}
    private void Start()
    {
        PrepareForNewText(_textBox); // Prepare for new text when the script starts
    }

    private void Update()
    {
        // If right mouse button is pressed
        if (Input.GetMouseButtonDown(1))
        {
            // If maxVisibleCharacters is not equal to the character count minus one...
            if (_textBox.maxVisibleCharacters != _textBox.textInfo.characterCount - 1)
                Skip(); // Call the Skip method to skip the typewriter effect
        }
    }

    // Method to prepare for new text
    private void PrepareForNewText(Object obj)
    {
        // If the object that triggered the event is not the text box, we don't need to do anything
        if (obj != _textBox || !_readyForNewText || _textBox.maxVisibleCharacters >= _textBox.textInfo.characterCount)
            return;

        CurrentlySkipping = false; // Set bool CurrentlySkipping to false when a new text is set
        _readyForNewText = false; // Set bool _readyForNewText to false when a new text is set

        // If there is a typewriter coroutine running, stop it
        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        _textBox.maxVisibleCharacters = 0; // Reset the maxVisibleCharacters to 0 to start typing from the beginning
        _currentVisibleCharacterIndex = 0; // Reset the currentVisibleCharacterIndex to 0 to start typing from the beginning

        _typewriterCoroutine = StartCoroutine(Typewriter()); // Start the typewriter coroutine to reveal the text
    }

    // IEnumerator for the typewriter effect
    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = _textBox.textInfo; // Get the text info of the text box

        // while currentVisibleCharacterIndex is less than the character count of the text info...
        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {
            var lastCharacterIndex = textInfo.characterCount - 1; // Get the last character index of the text info

            // If the current visible character index is greater than or equal to the last character index, we have revealed all characters
            if (_currentVisibleCharacterIndex >= lastCharacterIndex)
            {
                _textBox.maxVisibleCharacters++; // Increment the maxVisibleCharacters to reveal the last character
                yield return _textboxFullEventDelay; // Wait for a short delay before invoking the CompleteTextRevealed event
                CompleteTextRevealed?.Invoke(); // Invoke the CompleteTextRevealed event to notify that the text has been fully revealed
                _readyForNewText = true; // Set _readyForNewText to true to allow for new text to be set
                yield break; // Exit the coroutine as we have revealed all characters
            }

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character; // Get the character at the current visible character index

            _textBox.maxVisibleCharacters++; // Increment the maxVisibleCharacters to reveal the current character

            // If bool CurrentlySkipping is true and the character is not a question mark, a period, a comma, a colon, a semi-colon, a exclamation point, or a hyphen...
            if (!CurrentlySkipping &&
                (character == '?' || character == '.' || character == ',' || character == ':' ||
                 character == ';' || character == '!' || character == '-'))
            {
                yield return _interpunctuationDelay; // Wait for the interpunctuation delay before revealing the next character
            }
            else
            {
                yield return CurrentlySkipping ? _skipDelay : _simpleDelay; // Wait for the simple delay or skip delay based on whether CurrentlySkipping is true or false
            }

            CharacterRevealed?.Invoke(character); // Invoke the CharacterRevealed event to notify that a character has been revealed
            _currentVisibleCharacterIndex++; // Increment the current visible character index to move to the next character
        }
    }

    // Method to skip the typewriter effect
    private void Skip(bool quickSkipNeeded = false)
    {
        // If CurrentlySkipping is true...
        if (CurrentlySkipping)
            return; // Return early to avoid skipping multiple times

        CurrentlySkipping = true; // Set bool CurrentlySkipping to true

        // If quickSkip is false or quickSkipNeeded is false...
        if (!quickSkip || !quickSkipNeeded)
        {
            StartCoroutine(SkipSpeedupReset()); // Start the SkipSpeedupReset coroutine to reset CurrentlySkipping after a delay
            return; // Return early to avoid skipping the typewriter effect immediately
        }

        StopCoroutine(_typewriterCoroutine); // Stop the typewriter coroutine to skip the typewriter effect
        _textBox.maxVisibleCharacters = _textBox.textInfo.characterCount; // Set the maxVisibleCharacters to the character count to reveal all characters immediately
        _readyForNewText = true; //Set bool _readyForNewText to true
        CompleteTextRevealed?.Invoke(); // Invoke the CompleteTextRevealed event to notify that the text has been fully revealed
    }

    // IEnumerator to reset CurrentlySkipping after the typewriter effect is fully revealed
    private IEnumerator SkipSpeedupReset()
    {
        yield return new WaitUntil(() => _textBox.maxVisibleCharacters == _textBox.textInfo.characterCount - 1); // Wait until the typewriter effect is fully revealed
        CurrentlySkipping = false; // Set bool CurrentlySkipping to false
    }
}

