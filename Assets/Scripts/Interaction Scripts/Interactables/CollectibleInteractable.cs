using UnityEngine;

public class CollectibleInteractable : MonoBehaviour, ICollectable
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int collectType;

    public void Collect()
    {

        if (playerStats.currentEvent >= collectType)
        {
            Debug.Log("Collected");

            playerStats.AddCollectable(1);
            playerStats.StoreCollectible(gameObject);

            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Can't collect this yet");
        }

    }
}
