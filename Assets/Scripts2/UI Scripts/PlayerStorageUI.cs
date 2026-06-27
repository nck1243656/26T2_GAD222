using UnityEngine;
using UnityEngine.UI;

public class PlayerStorageUI : MonoBehaviour
{
    private Slider storageSlider;
    private PlayerStatus playerStatus;

    void Awake()
    {
        playerStatus= GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        storageSlider = GetComponent<Slider>();
    }

    private void FixedUpdate()
    {
        UpdateStorageUI();
    }

    void UpdateStorageUI()
    {
        storageSlider.maxValue = playerStatus.maxStorage;
        storageSlider.value = playerStatus.playerStorage;
    }

}
