using System;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GardensInitializer gardensInitializer;
    [SerializeField] private PlantsCollection collection;
    [SerializeField] private YandexAdsShower adsShower;
    [SerializeField] private YandexLoadDataHandler dataHandler;

    private const float saveTime = 5;

    private ISaver saver;

    private void OnEnable()
    {
        Upgrade.Applied += SaveData;
        dataHandler.RecieveData += LoadData;
    }

    private void OnDisable()
    {
        Upgrade.Applied -= SaveData;
        dataHandler.RecieveData -= LoadData;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }

    private void OnApplicationPause(bool isPaused)
    {
        if (isPaused)
            AudioListener.volume = 0f;
        else
            AudioListener.volume = 1f;
    }

    private void Start()
    {
        saver = new YandexSaver();
        collection.ResetCollection();
        dataHandler.RequestJson();
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
