using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GardensChanger : MonoBehaviour
{
    public event Action<Garden> ChangedActiveGarden;

    [SerializeField] private Camera camera;
    [SerializeField] private Transform movebleObject;
    [SerializeField] private GardensInitializer activator;
    [SerializeField] private GardenPresenter presenter;
    [SerializeField] private float sensitivity = 1;

    private Vector3 cursorPosition => camera.ScreenToWorldPoint(Input.mousePosition);

    private List<Garden> gardens = new List<Garden>();
    private Vector2 boundsX;
    private float startDragObjectPositionX, startDragCursorPositionX;

    public void OnEnable()
    {
        activator.GardenActivated += AddGarden;
    }

    private void OnDisable()
    {
        activator.GardenActivated -= AddGarden;
    }

    private void AddGarden(Garden garden)
    {
        if (gardens.Count == 0)
            Focus(garden);
        gardens.Add(garden);
        UpdateBounds(garden);
    }

    private void UpdateBounds(Garden garden)
    {
        var min = -(gardens[gardens.Count - 1].transform.position.x - gardens[gardens.Count - 1].transform.lossyScale.x / 2) + movebleObject.position.x;
        var max = gardens[0].transform.localPosition.x + gardens[0].transform.lossyScale.x / 2;
        boundsX = new Vector2(min, max);
    }

    public void OnBeginDrag(BaseEventData eventData)
    {
        startDragCursorPositionX = cursorPosition.x;
        startDragObjectPositionX = movebleObject.transform.position.x;
    }

    public void OnDrag(BaseEventData eventData)
    {
        var delta = startDragCursorPositionX - cursorPosition.x;
        MoveInBoundsTo(startDragObjectPositionX - delta * sensitivity);
    }

    public void OnEndDrag(BaseEventData eventData)
    {
        Focus(FindClosest());
    }

    private void MoveInBoundsTo(float positionX)
    {
        var xMoving = Mathf.Clamp(positionX, boundsX.x, boundsX.y);
        movebleObject.position = new Vector3(xMoving, movebleObject.position.y, movebleObject.position.z);
    }

    private void Focus(Garden garden)
    {
        MoveInBoundsTo(-transform.TransformPoint(garden.transform.localPosition).x);
        presenter.ChangeGarden(garden);
    }

    private Garden FindClosest()
    {
        Garden result = gardens[0];
        float closestDistance = float.MaxValue;
        for (var i = 0; i < gardens.Count; i++)
        {
            var distance = Vector2.Distance(camera.transform.position, gardens[i].transform.position);
            if (distance < closestDistance)
            {
                result = gardens[i];
                closestDistance = distance;
            }
        }
        return result;
    }

}
