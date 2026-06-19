using UnityEngine;
using TMPro;
public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    public float interactRadius = 0.5f;

    [Header("References")]
    public LayerMask interactLayer;
    [SerializeField] private TMPro.TextMeshPro interactionIndicator;

    private bool inRange = false;

    public bool canInteract = true;

    private void Start()
    {
        interactionIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!canInteract) return;

        ScanForInteractable();

        DisplayIndicator();

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryInteract();
        }
    }
 
    void ScanForInteractable()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position, interactRadius, interactLayer);

        inRange = detected.Length > 0;
    }

    void DisplayIndicator()
    {
        interactionIndicator.gameObject.SetActive(inRange);
    }

    void TryInteract()
    {
        if (!inRange)
        {
            Debug.Log("No interactables in range");
            return;
        }

        Collider[] detected = Physics.OverlapSphere(transform.position, interactRadius, interactLayer);

        foreach (Collider col in detected)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }

}
