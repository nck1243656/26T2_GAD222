using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private PlayerStatus playerStatus;

    private Image img;

    void Awake()
    {
        img = GetComponent<Image>();
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
    }

    private void FixedUpdate()
    {
        UpdateHealthUI();
    }

    void UpdateHealthUI()
    {
        img.sprite = sprites[playerStatus.playerHealth];
    }

}
