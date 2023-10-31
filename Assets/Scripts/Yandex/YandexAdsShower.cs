using System.Collections;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class YandexAdsShower : MonoBehaviour
{
    [SerializeField] private GameObject adWindow;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private int timeBeforeAd;

    private const int adsShowFrequency = 3;

    private int levelUpCount;

    [DllImport("__Internal")]
    private static extern void ShowFullscreenAd();

    private void OnEnable()
    {
        ShowFullscreenAd();
        Upgrade.Applied += TryShowAd;
    }

    private void OnDisable()
    {
        Upgrade.Applied -= TryShowAd;
    }

    private void TryShowAd(Upgrade upgrade)
    {
        levelUpCount++;
        if(levelUpCount == adsShowFrequency)
        {
            StartCoroutine(ShowAd());
            levelUpCount = 0;
            return;
        }
    }

    private IEnumerator ShowAd()
    {
        adWindow.SetActive(true);
        for (int i = timeBeforeAd; i > 0; i--)
        {
            timeText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        adWindow.SetActive(false);
        ShowFullscreenAd();
        yield break;

    }
}
