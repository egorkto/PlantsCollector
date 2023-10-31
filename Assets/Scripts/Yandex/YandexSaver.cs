using System.Runtime.InteropServices;
using UnityEngine;

public class YandexSaver : ISaver
{
    [DllImport("__Internal")]
    private static extern void SaveExtern(string data);

    public void Save(WorldData data)
    {
        var json = JsonUtility.ToJson(data);
        SaveExtern(json);
    }
}