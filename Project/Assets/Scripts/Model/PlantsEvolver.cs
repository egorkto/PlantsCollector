using System;
using UnityEngine;
using UnityEngine.Events;

public class PlantsEvolver : MonoBehaviour
{
    public event Action<EvolveEventData> Evolve;

    [SerializeField] private Plant[] plants;
    [SerializeField] private Garden garden;

    public Plant GetLowestTierPlant()
    {
        return plants[0];
    }

    public Plant[] GetPlants(string[] names)
    {
        var result = new Plant[names.Length];
        for(var i = 0; i < names.Length; i++)
        {
            var index = IndexOf(names[i]);
            if(index == -1)
                Debug.LogError($"There is not plant with name {names[i]}");
            result[i] = plants[index];

        }
        return result;
    }

    private void OnEnable()
    {
        garden.AddPlant += Subscribe;
        garden.RemovePlant += Unsubscribe;
    }

    private void OnDisable()
    {
        garden.AddPlant -= Subscribe;
        garden.RemovePlant -= Unsubscribe; 
    }

    private void Subscribe(Plant plant)
    {
        plant.FindMatch += TryEvolve;
    }

    private void Unsubscribe(Plant plant)
    {
        plant.FindMatch -= TryEvolve;
    }

    private void TryEvolve(Plant evolvedPlant, Plant joinedPlant)
    {
        var index = IndexOf(evolvedPlant.Name);
        if (index != -1 && TryGetNextPlant(index, out Plant nextPlant))
            Evolve?.Invoke(new EvolveEventData(nextPlant, new Plant[] { evolvedPlant, joinedPlant }));
    }

    private int IndexOf(string name)
    {
        for (int i = 0; i < plants.Length; i++)
        {
            if (name == plants[i].Name)
                return i;
        }
        return -1;
    }

    private bool TryGetNextPlant(int index, out Plant outPlant)
    {
        var success = index < plants.Length - 1;
        outPlant = success ? plants[index + 1] : null;
        return success;
    }

}
