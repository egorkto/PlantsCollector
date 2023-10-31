using UnityEngine;

public class IncreaseProfitIndex : Upgrade
{
    [SerializeField] private Player player;
    [SerializeField] private float profitIndexIncreasing;

    public override void OnApply()
    {
        player.IncreaseProfitIndex(profitIndexIncreasing);
    }
}
