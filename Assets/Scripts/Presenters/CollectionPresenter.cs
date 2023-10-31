using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectionPresenter : MonoBehaviour
{
    [SerializeField] private PlantsCollection collection;
    [SerializeField] private Text discoveryInformation;

    private void OnEnable()
    {
        collection.Activated += Colorize;
        collection.Reset += Reset;
        collection.DiscoveryCountChanged += ShowDiscoveredCount;
    }

    private void OnDisable()
    {
        collection.Activated -= Colorize;
        collection.Reset -= Reset;
        collection.DiscoveryCountChanged -= ShowDiscoveredCount;
    }

    private void Reset(CollectionElement[] elements)
    {
        foreach (var element in elements)
            element.SetColorChilds(new Color(0, 0, 0));
    }

    private void Colorize(CollectionElement plant)
    {
        plant.SetColorChilds(new Color(1, 1, 1));
    }

    private void ShowDiscoveredCount(int discovered, int count)
    {
        discoveryInformation.text =  discovered == count ? $"Вы собрали все растения!" : $"{discovered}/{count} Собрано";
    }
}
