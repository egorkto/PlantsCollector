using UnityEngine;

public class AddGarden : Upgrade
{
    [SerializeField] private GardensInitializer initializer;
     
    public override void OnApply()
    {
        initializer.TryInitializeNextGarden();
    }
}
