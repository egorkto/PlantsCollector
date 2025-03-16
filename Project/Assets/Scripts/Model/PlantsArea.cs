using UnityEngine;
using UnityEngine.UI;

public class PlantsArea : MonoBehaviour
{
    public Bounds Bounds => Refresh(bounds);

    [SerializeField] private Image border;

    private Bounds bounds;

    public void Initialize()
    {
        bounds = new Bounds(border.transform.position, border.transform.lossyScale * border.rectTransform.rect.size);
    }

    public Bounds Refresh(Bounds bounds)
    {
        bounds.center = border.transform.position;
        return bounds;
    }
}
