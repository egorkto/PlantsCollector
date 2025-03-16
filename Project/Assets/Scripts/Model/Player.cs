using System;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public event Action<double> ScoreChanged;
    public event Action<double> ProfitChanged;

    [SerializeField] private double score = 0;
    private double profit = 0;
    private double profitIndex = 1;

    public void IncreaseProfit(double value)
    {
        profit += value;
        ProfitChanged?.Invoke(profit * profitIndex);
    }

    public void DecreaseProfit(double value)
    {
        if(profit >= value)
        {
            profit -= value;
            ProfitChanged?.Invoke(profit * profitIndex);
        }
    }

    public void IncreaseProfitIndex(double value)
    {
        profitIndex += value;
        ProfitChanged?.Invoke(profit * profitIndex);
    }

    public bool CanBuy(double cost)
    {
        return score >= cost;
    }

    public void Buy(double cost)
    {
        if(CanBuy(cost))
        {
            score -= cost;
            ScoreChanged?.Invoke(score);
        }
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetMoney), 0, 1);
    }

    private void GetMoney()
    {
        score += profit * profitIndex;
        ScoreChanged?.Invoke(score);
    }

    private void OnEnable()
    {
        WorldDataConstructor.Construct += GiveData;
        LoadEventsHolder.UpdatePlayerData += SetData;
    }

    private void OnDisable()
    {
        WorldDataConstructor.Construct -= GiveData;
        LoadEventsHolder.UpdatePlayerData -= SetData;
    }


    private void GiveData(WorldData data)
    {
        data.PlayerData = new PlayerData()
        {
            Score = score,
            ProfitIndex = profitIndex
        };
    }

    private void SetData(PlayerData data)
    {
        score = data.Score;
        profitIndex = data.ProfitIndex;
        ScoreChanged?.Invoke(score);
    }
}
