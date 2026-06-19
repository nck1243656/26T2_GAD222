using UnityEngine;

public class DumpInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerStats playerStats;

    public void Interact()
    {
        Debug.Log("Healed + Dumped Collectibles");
        playerStats.playerHealth = playerStats.playerMaxHealth;
        playerStats.UpdateHeartsUI();
        playerStats.ClearCollectibles();
    }
}
