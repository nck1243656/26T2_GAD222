using UnityEngine;

public class PlayerCollection : MonoBehaviour
{
    [Header("Settings")]
    public float collectRadius = 0.1f;
    public Vector3 sphereOffset = new Vector3(0f, -0.3f, 0f);

    [Header("References")]
    public LayerMask collectLayer;
    [SerializeField] private TMPro.TextMeshPro collectibleIndicator;
    [SerializeField] private GameEvents gameEvents;

    private bool inRange = false;

    public bool canInteract = true;


    private void Start()
    {
        collectibleIndicator.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!canInteract) return;

        ScanForInteractable();

        DisplayIndicator();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            TryInteract();
        }
    }

    void ScanForInteractable()
    {
        Vector3 origin = transform.position + sphereOffset;
        Collider[] detected = Physics.OverlapSphere(origin, collectRadius, collectLayer);

        inRange = detected.Length > 0;
    }

    void DisplayIndicator()
    {
        collectibleIndicator.gameObject.SetActive(inRange);
    }

    void TryInteract()
    {
        if (!inRange)
        {
            Debug.Log("No collectables in range");
            return;
        }

        Vector3 origin = transform.position + sphereOffset;
        Collider[] detected = Physics.OverlapSphere(origin, collectRadius, collectLayer);

        foreach (Collider col in detected)
        {
            if (col.TryGetComponent<ICollectable>(out ICollectable collectable))
            {
                collectable.Collect();
                return;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position + sphereOffset;
        Gizmos.DrawWireSphere(origin, collectRadius);
    }
}
