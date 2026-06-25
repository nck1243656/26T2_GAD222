using UnityEngine;

public class CollectibleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private int collectType;

    public void Interact()
    {
        playerStatus.StoreCollectible(transform.parent.gameObject);
    }
}
