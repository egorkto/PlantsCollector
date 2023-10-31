using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPresenter : MonoBehaviour
{
    [SerializeField] private SpawnTimer timer;
    [SerializeField] private Image progressImage;
    [SerializeField] private Text text;

    private void Update()
    {
        progressImage.fillAmount = timer.PassedTime / timer.SpawnTime;
        text.text = Math.Round(timer.SpawnTime - timer.PassedTime, 1).ToString();
    }
}
