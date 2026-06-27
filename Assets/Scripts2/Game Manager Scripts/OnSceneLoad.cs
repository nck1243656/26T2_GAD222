using UnityEngine;

public class OnSceneLoad : MonoBehaviour
{
    private PlayerStatus playerStatus;

    [SerializeField] bool deleteCollectables = true;

    [SerializeField] Transform StartLocation;
    [SerializeField] GameObject Player;

    private void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        if (!playerStatus.gameStarted)
        {
            Player.transform.position = StartLocation.position;
            Player.transform.rotation = StartLocation.rotation;
        }
    }

    private void Start()
    {
        if (deleteCollectables)
        {
            playerStatus.DestroyCollectiblesOnLoad();
        }
    }
}
