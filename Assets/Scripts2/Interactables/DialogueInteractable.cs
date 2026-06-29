using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [SerializeField, TextArea(2, 5)] private List<string> dialogueStage0 = new List<string>();
    [SerializeField, TextArea(2, 5)] private List<string> dialogueStage1 = new List<string>();
    [SerializeField, TextArea(2, 5)] private List<string> dialogueStage2 = new List<string>();
    [SerializeField, TextArea(2, 5)] private List<string> dialogueStage3 = new List<string>();
    [SerializeField, TextArea(2, 5)] private List<string> dialogueStage4 = new List<string>();

    [SerializeField] private string ignoredText = "";

    [SerializeField] private List<string> currentDialogue;

    [Header("Settings")]
    [SerializeField, Range(0.01f, 0.1f)] 
    float typeSpeed = 0.03f;

    [SerializeField, Range(1f, 60f)]
    float interactTimeout = 5f;

    [SerializeField] int currentIndex = 0;

    [SerializeField] bool isImportant = false;
    //Finishing this characters dialogue + the collect amount has been met will progress game stage

    [Header("References")]
    [SerializeField] Transform mainCam;
    TextMeshPro dialogueBox;
    GameStage gameStage;
    PlayerStatus playerStatus;

    Coroutine timeoutCoroutine;

    bool isTyping = false;

    [SerializeField] bool isTalking = false;

    private void Awake()
    {
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        dialogueBox = GetComponentInChildren<TMPro.TextMeshPro>();

        currentDialogue = GetCurrentDialogue();
    }

    private void LateUpdate()
    {
        dialogueBox.transform.LookAt(mainCam);
        dialogueBox.transform.Rotate(0f, 180f, 0f);
    }

    public void Interact()
    {
        Debug.Log("Interacted with " + gameObject.name);

        if (isTalking)
        {
            Debug.Log("Setting Dialogue");
            currentDialogue = GetCurrentDialogue();
            isTalking = false;
        }

        if (!isTyping)
        {
            RestartTimeout();

            if (currentIndex >= currentDialogue.Count)
            {
                EndDialogue();
                return;
            }

            isTalking = true;
            StartCoroutine(TypeLine(currentDialogue[currentIndex]));
            currentIndex++;
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;    

        dialogueBox.text = "";

        foreach (char c in line)
        {
            dialogueBox.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
    }

    void RestartTimeout()
    {
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
        }

        timeoutCoroutine = StartCoroutine(TimeoutCroutine());
    }

    IEnumerator TimeoutCroutine()
    {
        Debug.Log("Start Timer");
        yield return new WaitForSeconds(interactTimeout);
        dialogueBox.text = ignoredText;
        yield return new WaitForSeconds(3f);
        EndDialogue();
    }

    void EndDialogue()
    {
        dialogueBox.text = "";
        currentIndex = 0;
        isTalking = false;

        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
            currentIndex = 0;
        }

        if (isImportant && playerStatus.amountCollected >= gameStage.currentAmountNeeded)
        {
            gameStage.ProgressGameStage();
        }
    }

    private List<string> GetCurrentDialogue()
    {
        switch (gameStage.currentGameStage)
        {
            case 1: return dialogueStage1;
            case 2: return dialogueStage2;
            case 3: return dialogueStage3;
            case 4: return dialogueStage4;
            default: return dialogueStage0;
        }
    }
}

