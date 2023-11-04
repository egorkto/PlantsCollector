using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class YandexHandler : MonoBehaviour
{
    public Action<WorldData> RecieveData;

    [DllImport("__Internal")]
    private static extern void CheckString(string str);

    public void SetJson(string json)
    {
        CheckString("Recieved");
        RecieveData?.Invoke(JsonUtility.FromJson<WorldData>(json));
    }

    public void OnAdOpen()
    {
        AudioListener.volume = 0f;
    }

    public void OnAdClose()
    {
        AudioListener.volume = 1f;
    }
}
