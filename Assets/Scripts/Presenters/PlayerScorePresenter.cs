using UnityEngine;
using UnityEngine.UI;

public class PlayerScorePresenter : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text totalProfit;

    private void OnEnable()
    {
        player.ScoreChanged += ShowScore;
        player.ProfitChanged += ShowProfit;
    }

    private void OnDisable()
    {
        player.ScoreChanged -= ShowScore;
        player.ProfitChanged -= ShowProfit;
    }

    private void ShowScore(double score)
    {
        scoreText.text = ScoreConverter.Convert(score);
    }

    private void ShowProfit(double profit)
    {
        totalProfit.text = ScoreConverter.Convert(profit) + "/c";
    }
}
