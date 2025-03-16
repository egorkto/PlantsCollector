using UnityEngine;

public class EvolveEventData
{
    public readonly Plant NextPlant;
    public readonly Vector2 EvolvePosition;
    public readonly Plant[] EvolvingPlants;

    public EvolveEventData(Plant nextPlant, Plant[] evolvingPlants)
    {
        NextPlant = nextPlant;
        EvolvingPlants = evolvingPlants;
        EvolvePosition = evolvingPlants[0].transform.position;
    }
}
