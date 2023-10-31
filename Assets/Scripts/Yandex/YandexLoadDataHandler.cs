using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexLoadDataHandler : MonoBehaviour
{
    public Action<WorldData> RecieveData;

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void CheckString(string str);

    public void RequestJson()
    {
        LoadExtern();
    }

    public void SetJson(string json)
    {
        CheckString(json);
        var loader = new YandexLoader(json);
        RecieveData?.Invoke(loader.Load());
    }
}
