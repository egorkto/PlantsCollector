using UnityEngine;

public class CollectionElement : MonoBehaviour
{
    [SerializeField] private Plant plant;

    public Plant Plant => plant;
    public bool Active => active;

    private bool active = false;

    public void Activate()
    {
        active = true;
    }

    public void SetColorChilds(Color color)
    {
        foreach (var child in GetComponentsInChildren<SpriteRenderer>())
            child.color = color;
    }
}
