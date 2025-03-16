using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Upgrade : MonoBehaviour
{
    public static event Action<Upgrade> Applied;

    public double Cost => prices[level];
    public int Level => level;
    public Text PriceText => text;

    [SerializeField] private double[] prices;
    [SerializeField] private Text text;

    private int level = 0;

    public void Apply()
    {
        OnApply();
        Applied?.Invoke(this);
    }

    public abstract void OnApply();

    public void SetLevel(int value)
    {
        if (value > 0 && value < prices.Length)
            level = value;
    }

    public void LevelUp()
    {
        level++;
    }

    public bool CanLevelUp()
    {
        return level < prices.Length;
    }
}
