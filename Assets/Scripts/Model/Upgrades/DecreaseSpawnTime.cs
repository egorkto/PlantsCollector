using UnityEngine;

public class DecreaseSpawnTime : Upgrade
{
    [SerializeField] private SpawnTimer timer;
    [SerializeField] private float decreasingSpawnTime;

    public override void OnApply()
    {
        timer.DecreaseSpawnTime(decreasingSpawnTime);
    }
}
