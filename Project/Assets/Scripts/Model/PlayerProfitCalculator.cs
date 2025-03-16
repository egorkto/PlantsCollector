using UnityEngine;

public class PlayerProfitCalculator : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Garden[] gardens;

    private void OnEnable()
    {
        foreach(var garden in gardens)
        {
            garden.AddPlant += (Plant plant) => player.IncreaseProfit(plant.Cost);
            garden.RemovePlant += (Plant plant) => player.DecreaseProfit(plant.Cost);
        }
    }

    private void OnDisable()
    {
        foreach (var garden in gardens)
        {
            garden.AddPlant -= (Plant plant) => player.IncreaseProfit(plant.Cost);
            garden.RemovePlant -= (Plant plant) => player.DecreaseProfit(plant.Cost);
        }
    }
}
