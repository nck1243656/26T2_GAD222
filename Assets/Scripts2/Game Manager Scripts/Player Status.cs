using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    [Header("Health")]
    public int playerHealth = 6;
    public int playerMaxHealth = 6;
    public event Action<int> PlayerHealthChanged;

    [Header("Storage")]
    public int playerStorage = 0;
    public int maxStorage = 4;

    public int amountCollected = 0;
    public event Action<int> OnAmountCollectedChanged;

    [Header("Stored Collectibles")]
    [SerializeField] private List<string> storedCollectibles = new List<string>();

    [SerializeField] private List<string> deadCollectibles = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void TakeDamage()
    {
        playerHealth--;

        //Player death
        if (playerHealth <= 0)
        {
            RestoreStorage();
            playerHealth = playerMaxHealth;
        }

        PlayerHealthChanged?.Invoke(playerHealth);
    }

    public void StatusReset()
    {
        playerHealth = playerMaxHealth;
        ClearStorage();
        PlayerHealthChanged?.Invoke(playerHealth);
    }

    public void StoreCollectible(GameObject obj)
    {
        if (playerStorage < maxStorage)
        {
            Debug.Log("Collected");

            string id = obj.name;

            Debug.Log($"Stored: {id}");
            storedCollectibles.Add(id);
            playerStorage++;

            ChildrenActiveStatus(obj, false);

            return;
        }

        Debug.Log("Storage Full");
    }

    void ClearStorage()
    {
        foreach (string id in storedCollectibles)
        {
            deadCollectibles.Add(id);

            amountCollected--;
            OnAmountCollectedChanged?.Invoke(amountCollected);

            GameObject obj = GameObject.Find(id);
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        storedCollectibles.Clear();
        playerStorage = 0;
    }

    void RestoreStorage()
    {
        foreach (string id in storedCollectibles)
        {
            amountCollected--;

            GameObject obj = GameObject.Find(id);
            ChildrenActiveStatus(obj, true);
        }
        storedCollectibles.Clear();
        playerStorage = 0;

        OnAmountCollectedChanged?.Invoke(amountCollected);
    }

    public void DestroyCollectiblesOnLoad()
    {
        foreach (string id in deadCollectibles)
        {
            GameObject obj = GameObject.Find(id);
            Destroy(obj);
        }
        foreach (string id in storedCollectibles)
        {
            GameObject obj = GameObject.Find(id);
            ChildrenActiveStatus(obj, false);
        }
    }

    void ChildrenActiveStatus(GameObject obj, bool state)
    {
        foreach (Transform child in obj.transform)
        {
            child.gameObject.SetActive(state);
        }
    }

    public void CountCollectible()
    {
        amountCollected++;
        OnAmountCollectedChanged?.Invoke(amountCollected);
    }
}
