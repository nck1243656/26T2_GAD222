using System;
using UnityEngine;

public class GameStage : MonoBehaviour
{
    [SerializeField, Range(0, 5)]
    public int currentGameStage = 0;
    public event Action<int, int> GameStageChange;

    [Header("Amount of Collectibles per Stage")]
    [SerializeField] public int amountNeededStage1;
    [SerializeField] public int amountNeededStage2;
    [SerializeField] public int amountNeededStage3;
    [SerializeField] public int amountNeededStage4;
    public int currentAmountNeeded;

    private void Start()
    {
        SetCurrentAmountNeeded();
    }

    public void ProgressGameStage()
    {
        currentGameStage++;

        SetCurrentAmountNeeded();

        GameStageChange?.Invoke(currentGameStage, currentAmountNeeded);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            ProgressGameStage();
        }
    }

    void SetCurrentAmountNeeded()
    {
        switch (currentGameStage)
        {
            case 1:
                currentAmountNeeded = amountNeededStage1;
                break;
            case 2:
                currentAmountNeeded = amountNeededStage2;
                break;
            case 3:
                currentAmountNeeded = amountNeededStage3;
                break;
            case 4:
                currentAmountNeeded = amountNeededStage4;
                break;
            default:
                currentAmountNeeded = 0;
                break;
        }
    }
}
