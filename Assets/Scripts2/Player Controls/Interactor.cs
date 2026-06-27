using UnityEngine;

interface IInteractable
{
    void Interact();
}
public class Interactor : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField, Range(0.1f, 10)]
    float interactRadius = 0.5f;

    [SerializeField, Range(0.1f, 10)]
    float collectRadius = 0.5f;

    [Header("References")]
    public LayerMask interactLayer;
    public LayerMask collectLayer;
    Keybinds keybinds;
    [SerializeField] GameObject interactionIndicator;

    public bool canInteract = true;

    private void Awake()
    {
        keybinds = GameObject.Find("GameManager").GetComponent<Keybinds>();
    }

    void Update()
    {
        SetIndicator();

        if (!canInteract) return; 

        InteractAvailable();

        if (Input.GetKeyDown(keybinds.interactKey))
        {
            Debug.Log("Attempting interaction");
            TryInteract();
        }

        if (Input.GetKeyDown(keybinds.collectKey))
        {
            Debug.Log("Attempting collection");
            TryCollect();
        }
    }
    
    void SetIndicator()
    {
        bool available = InteractAvailable();
        interactionIndicator.SetActive(available);
    }

    void TryInteract()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position, interactRadius, interactLayer);

        foreach (Collider col in detected)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }

        Debug.Log("Nothing in range");
    }

    void TryCollect()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position + Vector3.down * 0.5f, collectRadius, collectLayer);

        foreach (Collider col in detected)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                interactable.Interact();
                return;
            }
        }

        Debug.Log("Nothing in range");
    }

    bool InteractAvailable()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position, interactRadius, interactLayer);

        foreach (Collider col in detected)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                return true;
            }
        }
        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, interactRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.down * 0.5f, collectRadius);
    }
}
