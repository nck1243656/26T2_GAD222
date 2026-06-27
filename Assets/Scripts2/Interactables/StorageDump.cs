using UnityEngine;

public class StorageDump : MonoBehaviour, IInteractable
{
    private PlayerStatus playerStatus;

    void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
    }

    public void Interact()
    {
        playerStatus.StatusReset();
    }
}
