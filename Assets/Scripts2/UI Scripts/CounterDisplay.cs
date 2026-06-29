using UnityEngine;
using TMPro;

public class CounterDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI currentGoalText;
    [SerializeField] private TextMeshProUGUI currentCountText;
    private PlayerStatus playerStatus;
    private GameStage gameStage;

    [Header("Settings")]
    [SerializeField] private float growAmount;

    void Start()
    {
        playerStatus = GameObject.Find("GameManager").GetComponent<PlayerStatus>();
        gameStage = GameObject.Find("GameManager").GetComponent<GameStage>();

        playerStatus.OnAmountCollectedChanged += UpdateCountUI;
        gameStage.GameStageChange += UpdateGoalUI;
    }

    void UpdateCountUI(int amount)
    {
        currentCountText.text = amount.ToString();
    }

    void UpdateGoalUI(int stage, int amountNeeded)
    {
        currentGoalText.text = amountNeeded.ToString();
    }
}
