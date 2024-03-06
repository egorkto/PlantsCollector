using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GardensInitializer gardensInitializer;
    [SerializeField] private PlantsCollection collection;
    [SerializeField] private YandexAdsShower adsShower;
    [SerializeField] private YandexHandler yandexHandler;

    private const float saveTime = 5;

    private ISaver saver;
    private ILoader loader;
    private bool adOpen;

    private void OnEnable()
    {
        Upgrade.Applied += SaveData;
        yandexHandler.RecieveData += LoadData;
        yandexHandler.AdOpen += OnAdOpen;
        yandexHandler.AdClose += OnAdClose;
    }

    private void OnDisable()
    {
        Upgrade.Applied -= SaveData;
        yandexHandler.RecieveData -= LoadData;
        yandexHandler.AdOpen -= OnAdOpen;
        yandexHandler.AdClose -= OnAdClose;
    }

    private void OnAdOpen()
    {
        adOpen = true;
        AudioListener.volume = 0f;
    }

    private void OnAdClose()
    {
        adOpen = false;
        AudioListener.volume = 1f;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus && !adOpen)
            AudioListener.volume = 1f;
        else
            AudioListener.volume = 0f;
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused || adOpen)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }

    private void Start()
    {
        saver = new YandexSaver();
        loader = new YandexLoader();
        collection.ResetCollection();
        loader.Load();
        StartSaving(saveTime);
    }

    private void LoadData(WorldData data)
    {
        if (data.GardensData.Length == 0)
            StartNewGame();
        else
            LoadEventsHolder.UpdateData(data);
    }

    private void StartNewGame()
    {
        gardensInitializer.TryInitializeNextGarden();
    }

    private void StartSaving(float time)
    {
        InvokeRepeating(nameof(SaveData), 0, time);
    }

    private void SaveData(Upgrade upgrade)
    {
        saver.Save(WorldDataConstructor.ConstructData());
    }

    private void SaveData()
    {
        saver.Save(WorldDataConstructor.ConstructData());
    }
}
