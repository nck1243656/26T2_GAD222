using UnityEngine;
using TMPro;

public class InteractionIndicator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform mainCam;
    Keybinds keybinds;
    [SerializeField] bool staticCameraActive = false;

    private void Awake()
    {
        keybinds = GameObject.Find("GameManager").GetComponent<Keybinds>();
        TextMeshPro textMesh = GetComponent<TextMeshPro>();
        textMesh.text = $"{keybinds.interactKey}";
    }

    private void LateUpdate()
    {
        transform.LookAt(mainCam);
        if (!staticCameraActive)
        {
            transform.RotateAround(transform.position, transform.up, 180f);
        }
    }
}
