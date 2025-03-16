using UnityEngine;
using Random = UnityEngine.Random;

public class PlantsSpawner : MonoBehaviour
{
    [SerializeField] private SpawnTimer spawnTimer;
    [SerializeField] private ParticleSystem spawnEffect;

    private Garden garden;
    private Plant plant;

    public void StartSpawn(Garden garden, Plant plant)
    {
        this.garden = garden;
        this.plant = plant;
        spawnTimer.TimeUp += SpawnByTimer;
    }

    public void StopSpawn()
    {
        spawnTimer.TimeUp -= SpawnByTimer;
    }

    public void Spawn(Plant[] plants)
    {
        if (garden == null)
            Debug.LogError("Spawner has not initialized!");

        foreach (var plant in plants)
            Spawn(plant, GetRandomPosition(garden.PlantsArea.Bounds));
    }

    public void Spawn(Plant plant, Vector2 position)
    {
        var currentPlant = Instantiate(plant, new Vector3(position.x, position.y, 0), Quaternion.identity);
        Instantiate(spawnEffect, currentPlant.transform.position, Quaternion.identity);
        currentPlant.Initialize(garden.PlantsArea);
        currentPlant.transform.SetParent(garden.transform);
        garden.Add(currentPlant);
    }

    private void SpawnByTimer()
    {
        if (garden.HaveFreePlaces())
            Spawn(plant, GetRandomPosition(garden.PlantsArea.Bounds));
    }

    private Vector2 GetRandomPosition(Bounds bounds)
    {
        var x = Random.Range(bounds.min.x, bounds.max.x);
        var y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }
}