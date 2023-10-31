using UnityEngine;

public class YandexLoader : ILoader
{
    private string json;

    public YandexLoader(string data)
    {
        json = data;
    }

    public WorldData Load()
    {
        return JsonUtility.FromJson<WorldData>(json);
    }
}
