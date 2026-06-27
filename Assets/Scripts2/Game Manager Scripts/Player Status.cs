using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance { get; private set; }

    [Header("Health")]
    public int playerHealth = 6;
    public int playerMaxHealth = 6;

    [Header("Storage")]
    public int playerStorage = 0;
    public int maxStorage = 4;

    [Header("Stored Collectibles")]
    [SerializeField] private List<string> storedCollectibles = new List<string>();

    [SerializeField] private List<string> deadCollectibles = new List<string>();

    public bool gameStarted = false;

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

    private void Start()
    {
        gameStarted = true;
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
    }

    public void StatusReset()
    {
        playerHealth = playerMaxHealth;
        ClearStorage();
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
            GameObject obj = GameObject.Find(id);
            ChildrenActiveStatus(obj, true);
        }
        storedCollectibles.Clear();
        playerStorage = 0;
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
}
