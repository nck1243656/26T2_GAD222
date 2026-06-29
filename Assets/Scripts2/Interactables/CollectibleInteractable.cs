using System;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

public class CollectibleInteractable : MonoBehaviour, IInteractable
{
    private PlayerStatus playerStatus;
    private GameStage gameStage;

    [Header("Size (1–4). 1 = largest, 4 = smallest.")]
    [SerializeField] private int collectSize; // 1-4, 1 being largest

    [SerializeField] private string id;

    void Awake()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();

        ApplyID();
    }
    void ApplyID()
    {
        transform.parent.name = $"Collectible_{id}";
    }

    public void Interact()
    {
        if (collectSize <= gameStage.currentGameStage)
        {
            if (collectSize == gameStage.currentGameStage)
            {
                playerStatus.CountCollectible();
            }

            playerStatus.StoreCollectible(transform.parent.gameObject);

            return;
        }

        Debug.Log("Cant Pickup");
    }
}
