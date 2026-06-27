using UnityEngine;

public class Deathbox : MonoBehaviour
{
    [Header("References")]
    private PlayerStatus playerStatus;
    [SerializeField] private Transform resetPoint;

    void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player took damage");

        playerStatus.TakeDamage();

        other.transform.position = resetPoint.position;
    }
}
