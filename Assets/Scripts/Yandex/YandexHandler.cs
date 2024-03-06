using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexHandler : MonoBehaviour
{
    public event Action<WorldData> RecieveData;
    public event Action AdOpen;
    public event Action AdClose;

    [DllImport("__Internal")]
    private static extern void CheckString(string str);

    public void SetJson(string json)
    {
        CheckString("Recieved");
        RecieveData?.Invoke(JsonUtility.FromJson<WorldData>(json));
    }

    public void OnAdOpen()
    {
        AdOpen?.Invoke();
    }

    public void OnAdClose()
    {
        AdClose?.Invoke();
    }
}
