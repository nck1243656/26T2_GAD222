using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable
{
    [Header("Dialogue Reference")]
    [SerializeField] private Dialogue dialogueSystem;

    [Header("Dialogue Data")]
    [SerializeField] private string speakerName = "NPC Name";

    [TextArea(2, 5)]
    [SerializeField] private List<string> dialogueLines = new List<string>();

    [Header("Settings")]
    [SerializeField] private bool oneTimeUse = false;

    private bool hasInteracted = false;

    public void Interact()
    {
        if (oneTimeUse && hasInteracted) return;

        Debug.Log("Interacted with: " + gameObject.name);

        dialogueSystem.StartDialogue(speakerName, dialogueLines);

        hasInteracted = true;
    }
}
