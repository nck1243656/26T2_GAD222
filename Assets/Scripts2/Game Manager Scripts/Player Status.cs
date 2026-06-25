using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    [Header("Health")]
    public int playerHealth = 6;
    public int playerMaxHealth = 6;

    [Header("Storage")]
    public int playerStorage = 0;
    public int maxStorage = 4;

    [Header("Stored Collectibles")]
    [SerializeField] private List<GameObject> storedCollectibles = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            RestoreStorage();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearStorage();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TakeDamage();
        }
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

    public void StoreCollectible(GameObject obj)
    {
        if (playerStorage < maxStorage)
        {
            Debug.Log("Collected");

            storedCollectibles.Add(obj);
            playerStorage++;
            obj.SetActive(false);

            return;
        }

        Debug.Log("Storage Full");
    }

    void ClearStorage()
    {
        foreach (GameObject obj in storedCollectibles)
        {
            Destroy(obj);
        }
        storedCollectibles.Clear();
        playerStorage = 0;
    }

    void RestoreStorage()
    {
        foreach (GameObject obj in storedCollectibles)
        {
            obj.SetActive(true);
        }
        storedCollectibles.Clear();
        playerStorage = 0;
    }
}
