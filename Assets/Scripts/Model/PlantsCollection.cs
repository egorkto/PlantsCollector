using System;
using UnityEngine;

public class PlantsCollection : MonoBehaviour
{
    public event Action<CollectionElement> Activated;
    public event Action<CollectionElement[]> Reset;
    public event Action<int, int> DiscoveryCountChanged;

    [SerializeField] private Garden[] gardens;
    [SerializeField] private CollectionElement[] elements;

    private int discoveryCount;

    public void ResetCollection()
    {
        Reset?.Invoke(elements);
    }

    private void OnEnable()
    {
        LoadEventsHolder.UpdateCollectionData += UpdateData;
        WorldDataConstructor.Construct += GiveData;
        foreach (var garden in gardens)
            garden.AddPlant += TryActivateElement;
    }

    private void OnDisable()
    {
        LoadEventsHolder.UpdateCollectionData -= UpdateData;
        WorldDataConstructor.Construct -= GiveData;
        foreach (var garden in gardens)
            garden.AddPlant -= TryActivateElement;
    }

    private void TryActivateElement(Plant plant)
    {
        foreach (var element in elements)
            if (element.Plant == plant && element.Active == false)
                Activate(element);
    }

    private void UpdateData(bool[] data)
    {
        for (var i = 0; i < data.Length; i++)
            if(data[i])
                Activate(elements[i]);
    }

    private void GiveData(WorldData data)
    {
        data.CollectionData = new bool[elements.Length];
        for (int i = 0; i < data.CollectionData.Length; i++)
            data.CollectionData[i] = elements[i].Active;
    }

    private void Activate(CollectionElement element)
    {
        discoveryCount++;
        element.Activate();
        Activated?.Invoke(element);
        DiscoveryCountChanged?.Invoke(discoveryCount, elements.Length);
    }
}
