using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private UpgradesPresenter presenter;
    [SerializeField] private Upgrade[] upgrades;

    private void Start()
    {
        foreach (var upgrade in upgrades)
            presenter.Present(upgrade);
    }

    public void Click(Upgrade upgrade)
    {
        if (TryBuy(upgrade))
            upgrade.Apply();
    }

    private bool TryBuy(Upgrade upgrade)
    {
        if(player.CanBuy(upgrade.Cost) && upgrade.CanLevelUp())
        {
            player.Buy(upgrade.Cost);
            upgrade.LevelUp();
            return true;
        }
        return false;
    }

    private void OnEnable()
    {
        LoadEventsHolder.UpdateUpgradesData += SetData;
        WorldDataConstructor.Construct += GiveData;
    }

    private void OnDisable()
    {
        LoadEventsHolder.UpdateUpgradesData -= SetData;
        WorldDataConstructor.Construct -= GiveData;
    }

    public void SetData(int[] data)
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            upgrades[i].SetLevel(data[i]);
            presenter.Present(upgrades[i]);
        }
    }

    public void GiveData(WorldData data)
    {
        data.UpgradesData = new int[upgrades.Length];
        for (int i = 0; i < upgrades.Length; i++)
            data.UpgradesData[i] = upgrades[i].Level;
    }
}