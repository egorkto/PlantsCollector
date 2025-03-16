using UnityEngine;
using UnityEngine.UI;

public class GardenPresenter : MonoBehaviour
{
    [SerializeField] private Text text;

    private Garden garden;

    public void ChangeGarden(Garden garden)
    {
        this.garden = garden;
        Present(garden);
        garden.ChangePlacesCount += Present;
    }

    private void OnDisable()
    {
        if(garden != null)
            garden.ChangePlacesCount -= Present;
    }

    private void Present(Garden garden)
    {
        text.text = $"{garden.OccupiedPlaces}/{garden.Places}";
    }
}
