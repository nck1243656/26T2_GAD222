using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI continuePrompt;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private TMPro.TextMeshPro interactionIndicator;

    [Header("Settings")]
    [SerializeField] private float typeSpeed = 0.03f;

    private Queue<string> lines = new Queue<string>();

    private bool isTyping;
    private bool dialogueActive;
    private string currentLine;

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (!dialogueActive) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogue.text = currentLine;
                isTyping = false;
                continuePrompt.gameObject.SetActive(true);
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(string speakerName, List<string> dialogueLines)
    {
        dialogueActive = true;

        playerInteraction.canInteract = false;
        interactionIndicator.enabled = false;
        playerMovement.enabled = false;

        dialogueBox.SetActive(true);
        characterName.text = speakerName;

        lines.Clear();

        foreach (string line in dialogueLines)
            lines.Enqueue(line);

        NextLine();
    }

    private void NextLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine = lines.Dequeue();
        StartCoroutine(TypeLine(currentLine));
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        continuePrompt.gameObject.SetActive(false);
        dialogue.text = "";

        foreach (char c in line)
        {
            dialogue.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        isTyping = false;
        continuePrompt.gameObject.SetActive(true);
    }

    private void EndDialogue()
    {
        dialogueActive = false;

        dialogueBox.SetActive(false);

        StartCoroutine(ReEnableInteraction());
    }

    private IEnumerator ReEnableInteraction()
    {
        yield return new WaitForSeconds(0.1f);
        playerInteraction.canInteract = true;
        interactionIndicator.enabled = true;
        playerMovement.enabled = true;
    }
}

