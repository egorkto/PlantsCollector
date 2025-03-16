using System;
using System.Collections.Generic;
using UnityEngine;

public class GardensInitializer : MonoBehaviour
{
    public event Action<Garden> GardenActivated;

    [SerializeField] private int places;
    [SerializeField] private Garden[] gardens;

    private int currentGardenIndex = 0;

    public Garden TryInitializeNextGarden()
    {
        if (currentGardenIndex >= gardens.Length)
            Debug.LogError("Доступные огороды закончились!");

        var garden = gardens[currentGardenIndex];
        garden.Initialize(places);
        GardenActivated?.Invoke(garden);
        currentGardenIndex++;
        return garden;
    }

    public void IncreasePlaces()
    {
        places++;
        foreach (var garden in gardens)
            garden.IncreasePlaces();
    }

    private void OnEnable()
    {
        LoadEventsHolder.UpdateGadensData += SetData;
        WorldDataConstructor.Construct += GiveData;
    }

    private void OnDisable()
    {
        LoadEventsHolder.UpdateGadensData -= SetData;
        WorldDataConstructor.Construct -= GiveData;
    }

    public void GiveData(WorldData data)
    {
        var gardensData = new List<GardenData>();
        for (int i = 0; i < gardens.Length; i++)
            if(gardens[i].Initialized)
                gardensData.Add(gardens[i].GetData());
        data.GardensPlaces = places;
        data.GardensData = gardensData.ToArray();
    }

    public void SetData(GardenData[] dataList, int places)
    {
        this.places = places;
        foreach (var data in dataList)
        {
            var garden = TryInitializeNextGarden();
            garden.SetData(data);
        }
    }
}
