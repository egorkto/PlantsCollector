using UnityEngine;

public class IncreasePlacesCount : Upgrade
{
    [SerializeField] private GardensInitializer initializer;

    public override void OnApply()
    {
        initializer.IncreasePlaces();
    }
}
