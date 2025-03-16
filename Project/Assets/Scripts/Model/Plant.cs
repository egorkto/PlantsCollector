using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlantMover))]
[RequireComponent(typeof(CircleCollider2D))]
public class Plant : MonoBehaviour
{
    public event Action<Plant, Plant> FindMatch;

    public int Cost => cost;
    public string Name => name;

    [SerializeField] private int cost;
    [SerializeField] private string name;

    private CircleCollider2D collider;
    private PlantMover mover;
    private LayerMask plantLayer;

    public void Initialize(PlantsArea moveArea)
    {
        mover = GetComponent<PlantMover>();
        collider = GetComponent<CircleCollider2D>();
        mover.Initialize(moveArea);
        plantLayer = (int)Mathf.Pow(2, gameObject.layer);
        mover.MouseUp += TryFindMatch;
    }

    private void OnDestroy()
    {
        mover.MouseUp -= TryFindMatch;
    }

    private void TryFindMatch()
    {
        var radius = collider.radius * transform.lossyScale.x;
        var joinings = from joining in Physics2D.OverlapCircleAll(transform.position, radius, plantLayer) where joining != collider select joining;
        var candidates = from candidate in joinings where candidate.GetComponent<Plant>() == this select candidate.GetComponent<Plant>();
        if (candidates.Count() > 0)
            FindMatch?.Invoke(this, candidates.First());
    }

    public static bool operator ==(Plant plant1, Plant plant2)
    {
        return plant1.name == plant2.name;
    }

    public static bool operator !=(Plant plant1, Plant plant2)
    {
        return plant1.name != plant2.name;
    }
}
