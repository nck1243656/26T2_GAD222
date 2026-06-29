using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    private PlayerStatus playerStatus;
    private GameStage gameStage;

    [SerializeField] bool deleteCollectables = true;

    [SerializeField] Transform StartLocation;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();
    }

    private void Start()
    {
        if (deleteCollectables)
        {
            playerStatus.DestroyCollectiblesOnLoad();
        }

        if (gameStage.currentGameStage == 0)
        {
            Debug.Log("teleport player");
            Player.transform.position = StartLocation.position;
            Player.transform.rotation = StartLocation.rotation;
        }
    }
}
