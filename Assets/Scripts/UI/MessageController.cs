using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class MessageController : MonoBehaviour
{
    [Header("Message Settings")]

    /// <summary>
    /// Duration of message fade transition
    /// </summary>
    [SerializeField]
    private float messageTransitionDuration = 1;

    /// <summary>
    /// UI Text element to show message.text in
    /// </summary>
    [SerializeField]
    private Text mainTextElement;

    /// <summary>
    /// Smaller UI Text element to show message.subtext in
    /// </summary>
    [SerializeField]
    private Graphic subTextElement;
    
    /// <summary>
    /// Message background
    /// </summary>
    [SerializeField] private Graphic backgroundGraphic;

    [Range(0,1)]
    [SerializeField] private float backgroundAlpha;

    /// <summary>
    /// List of messages to show
    /// </summary>
    [SerializeField]
    private List<Message> messages;


    [Header("Left Click Indicator Settings")]

    /// <summary>
    /// Icon that tells the player to click
    /// </summary>
    [SerializeField]
    private Graphic leftClickIndicator;
    
    /// <summary>
    /// Time to wait before showing left click indicator
    /// </summary>
    [SerializeField]
    private float leftClickDelay;

    /// <summary>
    /// Duration of fade in/out transition for left click indicator
    /// </summary>
    [SerializeField]
    private float leftClickFadeDuration;

    /// <summary>
    /// Time to keep left click indicator on screen for
    /// </summary>
    [SerializeField]
    private float leftClickShowDuration;

    private int currentMessageIndex = 0;

    void Start()
    {
        //StartCoroutine(ShowScrollWheelIndicator(leftClickDelay, leftClickFadeDuration, leftClickShowDuration));

        SetAlpha(mainTextElement,0);
        SetAlpha(subTextElement,0);
        SetAlpha(backgroundGraphic,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMessageIndex < messages.Count)
        {
            // Show next message when coroutine ends
            if (showmessageCoroutine == null)
            {
                Message message = messages[currentMessageIndex];
                showmessageCoroutine = StartCoroutine(ShowMessage(message));
            }
        }
    }

    /// <summary>
    /// Running instance of ShowMessage (null if not currently running)
    /// </summary>
    private Coroutine showmessageCoroutine;

    /// <summary>
    /// Shows a message on the screen with fade in/fade out transitions
    /// </summary>
    /// <param name="message">Message to show</param>
    IEnumerator ShowMessage(Message message)
    {
        // Update UI with message text/font settings
        mainTextElement.text = message.mainText;
        mainTextElement.fontSize = message.mainTextSize;
        mainTextElement.font = message.mainTextFont;

        // Wait for delay
        yield return new WaitForSeconds(message.delay);
        
        // Fade in main/sub text
        StartCoroutine(FadeIn(mainTextElement, 0, 1));
        StartCoroutine(FadeIn(subTextElement, 0, 1));

        // Fade background
        StartCoroutine(FadeIn(backgroundGraphic, 0, backgroundAlpha));

        // Wait for both messages to appear on screen
        yield return new WaitForSeconds(messageTransitionDuration);

        // Wait for duration of message duration
        yield return new WaitForSeconds(message.duration);

        // Fade out main/sub text
        StartCoroutine(FadeIn(mainTextElement, 1,  0));
        StartCoroutine(FadeIn(subTextElement, 1, 0));

        // Fade background
        StartCoroutine(FadeIn(backgroundGraphic, backgroundAlpha,  0));

        // Wait for both messages to disappear from screen
        yield return new WaitForSeconds(messageTransitionDuration);

        // Increment current message index
        currentMessageIndex++;

        // Set coroutine instance to null before returning
        showmessageCoroutine = null;
        yield return null;
    }

    /// <summary>
    /// Smoothly fades a graphic from invisible to opaque
    /// </summary>
    /// <param name="element">UI element to fade</param>
    /// <returns></returns>
    IEnumerator FadeIn(Graphic element, float startALpha, float targetAlpha, float delay=0)
    {
        // Wait delay before fading
        yield return new WaitForSeconds(delay);

        // Fade in
        for (float t = 0; t < 1; t+= Time.deltaTime / messageTransitionDuration) {
            float alpha = Mathf.Lerp(startALpha, targetAlpha, t);
            SetAlpha(element, alpha);

            yield return null;
        }
        SetAlpha(element, targetAlpha);
    }

    void SetAlpha(Graphic element, float alpha)
    {
        Color color = element.color;
        color.a = alpha;
        element.color = color;
    }
}
