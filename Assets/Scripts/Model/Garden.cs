using System;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public event Action<Plant> AddPlant;
    public event Action<Plant> RemovePlant;
    public event Action<Garden> ChangePlacesCount;

    public PlantsArea PlantsArea => plantsArea;
    public int Places => places;
    public int OccupiedPlaces => plants.Count;
    public bool Initialized { get; private set; } = false;

    [SerializeField] private PlantsSpawner spawner;
    [SerializeField] private PlantsEvolver evolver;
    [SerializeField] private PlantsArea plantsArea;

    private int places;

    private List<Plant> plants = new List<Plant>();

    public void Initialize(int places)
    {
        plantsArea.Initialize();
        spawner.StartSpawn(this, evolver.GetLowestTierPlant());
        this.places = places;
        Initialized = true;
    }

    public void Add(Plant plant)
    {
        plants.Add(plant);
        ChangePlacesCount?.Invoke(this);
        AddPlant?.Invoke(plant);
    }

    public void IncreasePlaces()
    {
        places++;
        ChangePlacesCount?.Invoke(this);
    }

    public GardenData GetData()
    {
        var data = new GardenData() { PlantsData = new string[plants.Count] };
        for (int i = 0; i < data.PlantsData.Length; i++)
            data.PlantsData[i] = plants[i].Name;
        return data;
    }

    public void SetData(GardenData data)
    {
        spawner.Spawn(evolver.GetPlants(data.PlantsData));
        ChangePlacesCount?.Invoke(this);
    } 

    public bool HaveFreePlaces()
    {
        return Places > plants.Count;
    }

    public bool Has(Plant plant)
    {
        return plants.Contains(plant);
    }
    private void OnEnable()
    {
        evolver.Evolve += OnEvolve;
    }

    private void OnDisable()
    {
        evolver.Evolve -= OnEvolve;
    }

    private void OnEvolve(EvolveEventData evolveEventData)
    {
        spawner.Spawn(evolveEventData.NextPlant, evolveEventData.EvolvePosition);
        foreach (var plant in evolveEventData.EvolvingPlants)
            Remove(plant);
    }

    private void Remove(Plant plant)
    {
        plants.Remove(plant);
        Destroy(plant.gameObject);
        ChangePlacesCount?.Invoke(this);
        RemovePlant?.Invoke(plant);
    }
}
