using UnityEngine;

public class StorageDump : MonoBehaviour, IInteractable
{
    private PlayerStatus playerStatus;
    private GameStage gameStage;

    void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();

        if (gameStage.currentGameStage == 0)
        {
            Destroy(this);
        }
    }

    public void Interact()
    {
        playerStatus.StatusReset();
    }
}
