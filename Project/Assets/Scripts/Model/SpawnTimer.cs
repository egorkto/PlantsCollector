using System;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    public event Action TimeUp;

    public float SpawnTime => spawnTime;
    public float PassedTime => passedTime;

    [SerializeField] private float spawnTime;
    [SerializeField] private float reduceTimeOnClick;

    private float passedTime;

    public void DecreaseSpawnTime(float value)
    {
        spawnTime -= value;
    }

    public void ReduceSpawnTime()
    {
        passedTime += reduceTimeOnClick;
    }

    private void Update()
    {
        if (passedTime >= spawnTime)
        {
            TimeUp?.Invoke();
            passedTime = 0;
        }
        else
        {
            passedTime += Time.deltaTime;
        }
    }
}
