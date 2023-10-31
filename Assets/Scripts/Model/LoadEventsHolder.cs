using System;
using System.Collections.Generic;
using UnityEngine;

public static class LoadEventsHolder
{
    public static event Action<PlayerData> UpdatePlayerData;
    public static event Action<int[]> UpdateUpgradesData;
    public static event Action<bool[]> UpdateCollectionData;
    public static event Action<GardenData[], int> UpdateGadensData;

    public static void UpdateData(WorldData data)
    {
        UpdateUpgradesData?.Invoke(data.UpgradesData);
        UpdatePlayerData?.Invoke(data.PlayerData);
        UpdateGadensData?.Invoke(data.GardensData, data.GardensPlaces);
        UpdateCollectionData?.Invoke(data.CollectionData);
    }
}
