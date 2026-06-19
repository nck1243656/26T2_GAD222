using UnityEngine;

public class Deathbox : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform resetPoint;
    [SerializeField] private PlayerStats playerStats;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player Fell");
        playerStats.TakeDamage(1);

        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            controller.enabled = false;
            other.transform.position = resetPoint.position;
            controller.enabled = true;
        }
        else
        {
            other.transform.position = resetPoint.position;
        }
    }
}
