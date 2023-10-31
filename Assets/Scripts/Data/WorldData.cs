using System;

[Serializable]
public class WorldData
{
    public PlayerData PlayerData;
    public int[] UpgradesData;
    public bool[] CollectionData;
    public int GardensPlaces;
    public GardenData[] GardensData;

    public override string ToString()
    {
        return $"Player: {PlayerData.Score}\n" + $"Upgrades: {UpgradesData}\n" + $"Collection: {CollectionData}\n" + $"Gardens: {GardensData}";
    }
}
