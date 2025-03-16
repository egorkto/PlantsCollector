using UnityEngine;

public class GameSetup : MonoBehaviour
{
    [SerializeField] private GardensInitializer gardensInitializer;
    [SerializeField] private PlantsCollection collection;

    private const float saveTime = 5;

    private ISaver saver;
    private ILoader loader;
    private bool adOpen;

    private void OnEnable()
    {
        Upgrade.Applied += SaveData;
    }

    private void OnDisable()
    {
        Upgrade.Applied -= SaveData;
    }

    private void Start()
    {
        collection.ResetCollection();
        var path = Application.persistentDataPath + "/save.json";
        loader = new LocalRepositoryLoader(path);
        saver = new LocalRepositorySaver(path);
        var data = loader.Load();
        LoadData(data);
        StartSaving(saveTime);
    }

    private void LoadData(WorldData data)
    {
        if (data == null)
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
        SaveData();
    }

    private void SaveData()
    {
        saver.Save(WorldDataConstructor.ConstructData());
    }
}
