using UnityEngine;

public class UpgradesPresenter : MonoBehaviour
{
    public void Present(Upgrade upgrade)
    {
        upgrade.PriceText.text = upgrade.CanLevelUp() ? ScoreConverter.Convert(upgrade.Cost) : "Max"; 
    }

    private void OnEnable()
    {
        Upgrade.Applied += Present;
    }

    private void OnDisable()
    {
        Upgrade.Applied += Present;
    }

}
