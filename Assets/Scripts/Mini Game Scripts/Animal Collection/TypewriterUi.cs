using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TypewriterUi : MonoBehaviour
{
    Text _text;
    TMP_Text _tmpProText;
    string writer;

    [SerializeField] float delayBeforeStart = 0f;
    [SerializeField] float timeBtwChars = 0.01f;
    [SerializeField] string leadingChar = "";
    [SerializeField] bool leadingCharBeforeDelay = false;

    private bool isTyping = false; // Tracks if typing is in progress
    private bool isTypingComplete = false; // Tracks if typing is complete
    private Coroutine typingCoroutine; // Reference to the typing coroutine

    public bool IsTypingComplete => isTypingComplete; // Expose typing state for external scripts


    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>()!;
        _tmpProText = GetComponent<TMP_Text>()!;

        if (_text != null)
        {
            writer = _text.text;
            _text.text = "";

            typingCoroutine = StartCoroutine("TypeWriterText");
        }

        if (_tmpProText != null)
        {
            writer = _tmpProText.text;
            _tmpProText.text = "";

            typingCoroutine = StartCoroutine("TypeWriterTMP");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Detect left mouse click
        {
            if (isTyping) // If typing is in progress, finish it immediately
            {
                FinishTyping();
            }
        }
    }

    IEnumerator TypeWriterText()
    {
        isTyping = true; // Set typing state to true
        _text.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (_text.text.Length > 0)
            {
                _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
            }
            _text.text += c;
            _text.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _text.text = _text.text.Substring(0, _text.text.Length - leadingChar.Length);
        }

        isTyping = false; // Set typing state to false
        isTypingComplete = true; // Set typing complete state to true
    }

    IEnumerator TypeWriterTMP()
    {
        isTyping = true; // Set typing state to true
        _tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

        foreach (char c in writer)
        {
            if (_tmpProText.text.Length > 0)
            {
                _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
            }
            _tmpProText.text += c;
            _tmpProText.text += leadingChar;
            yield return new WaitForSeconds(timeBtwChars);
        }

        if (leadingChar != "")
        {
            _tmpProText.text = _tmpProText.text.Substring(0, _tmpProText.text.Length - leadingChar.Length);
        }

        isTyping = false; // Set typing state to false
        isTypingComplete = true; // Set typing complete state to true
    }

    private void FinishTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Stop the typing coroutine
        }

        if (_text != null)
        {
            _text.text = writer; // Immediately display the full text
        }

        if (_tmpProText != null)
        {
            _tmpProText.text = writer; // Immediately display the full text
        }

        isTyping = false;
        isTypingComplete = true;
    }
}
