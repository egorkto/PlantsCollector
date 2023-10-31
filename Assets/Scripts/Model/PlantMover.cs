using System;
using UnityEngine;

public class PlantMover : MonoBehaviour
{
    public event Action MouseUp;

    private Camera camera;
    private PlantsArea moveArea;

    public void Initialize(PlantsArea bounds)
    {
        moveArea = bounds;
    }

    private void Start()
    {
        camera = Camera.main;
    }

    private void OnMouseDrag()
    {
        var cursor = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        transform.position = new Vector2(Mathf.Clamp(cursor.x, moveArea.Bounds.min.x, moveArea.Bounds.max.x), 
                                         Mathf.Clamp(cursor.y, moveArea.Bounds.min.y, moveArea.Bounds.max.y));
    }

    private void OnMouseUp()
    {
        MouseUp?.Invoke();
    }
}
