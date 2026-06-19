using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public int playerHealth = 4;
    public int playerMaxHealth = 4;

    [Header("Storage")]
    public int playerStorage = 0;
    public int maxStorage = 4;

    [Header("References")]
    [SerializeField] private RawImage[] hearts;
    [SerializeField] private Slider storageSlider;
    [SerializeField] private PlayerCollection playerCollection;
    [SerializeField] private TMPro.TextMeshPro collectibleIndicator;

    [Header("Stored Collectibles")]
    [SerializeField] private List<GameObject> storedCollectibles = new List<GameObject>();

    public int currentEvent = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            changeEvent(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            changeEvent(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            changeEvent(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            changeEvent(4);
        }
    }

    public void changeEvent(int amount)
    {
        currentEvent = amount;
        Debug.Log("Changed Event to " + amount);
    }

    private void Start()
    {
        UpdateHeartsUI();
        UpdateStorageUI();
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, hearts.Length);

        UpdateHeartsUI();

        if (playerHealth <= 0)
        {
            RestoreCollectibles();
            ResetHealth();
        }
    }

    public void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < playerHealth;
        }
    }

    void ResetHealth()
    {
        playerHealth = playerMaxHealth;
        UpdateHeartsUI();
    }




    public void AddCollectable(int amount)
    {
        playerStorage += amount;
        playerStorage = Mathf.Clamp(playerStorage, 0, maxStorage);

        UpdateStorageUI();

        CheckStorageState();
    }
    void UpdateStorageUI()
    {
        storageSlider.maxValue = maxStorage;
        storageSlider.value = playerStorage;
    }

    void CheckStorageState()
    {
        if (playerStorage >= maxStorage)
        {
            playerCollection.canInteract = false;
            collectibleIndicator.gameObject.SetActive(false);
        }
        else
        {
            playerCollection.canInteract = true;
            collectibleIndicator.gameObject.SetActive(true);
        }
    }

    public void StoreCollectible(GameObject obj)
    {
        if (!storedCollectibles.Contains(obj))
        {
            storedCollectibles.Add(obj);
        }
    }

    void RestoreCollectibles()
    {
        foreach (GameObject obj in storedCollectibles)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }

        storedCollectibles.Clear();

        playerStorage = 0;
        UpdateStorageUI();

        CheckStorageState();
    }

    public void ClearCollectibles()
    {
        foreach (GameObject obj in storedCollectibles)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        storedCollectibles.Clear();

        playerStorage = 0;
        UpdateStorageUI();

        playerCollection.canInteract = true;
    }
}
