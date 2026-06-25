using UnityEngine;
using TMPro;

public class InteractionIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform mainCam;
    [SerializeField] Keybinds keybinds;

    private void Awake()
    {
        TextMeshPro textMesh = GetComponent<TextMeshPro>();
        textMesh.text = $"{keybinds.interactKey}";
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCam);
        transform.RotateAround(transform.position, transform.up, 180f);
    }
}
