using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueInteractable : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [TextArea(2, 5)]
    [SerializeField] private List<string> dialogueLines = new List<string>();

    [SerializeField] private string ignoredText = "";

    [Header("Settings")]
    [SerializeField, Range(0.01f, 0.1f)] 
    float typeSpeed = 0.03f;

    [SerializeField, Range(1f, 60f)]
    float interactTimeout = 5f;

    int currentIndex = 0;

    [Header("References")]
    [SerializeField] Transform mainCam;
    TextMeshPro dialogueBox;

    Coroutine timeoutCoroutine;

    private void Awake()
    {
        dialogueBox = GetComponentInChildren<TMPro.TextMeshPro>();
    }

    private void LateUpdate()
    {
        dialogueBox.transform.LookAt(mainCam);
        dialogueBox.transform.Rotate(0f, 180f, 0f);
    }

    public void Interact()
    {

        Debug.Log("Interacted with " + gameObject.name);

        RestartTimeout();

        if (currentIndex >= dialogueLines.Count)
        {
            EndDialogue();
            return;
        }

        StartCoroutine(TypeLine(dialogueLines[currentIndex]));
        currentIndex++;
    }

    IEnumerator TypeLine(string line)
    {
        dialogueBox.text = "";

        foreach (char c in line)
        {
            dialogueBox.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
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
        if (timeoutCoroutine != null)
        {
            StopCoroutine(timeoutCoroutine);
            timeoutCoroutine = null;
        }

        dialogueBox.text = "";
        currentIndex = 0;
    }
}

