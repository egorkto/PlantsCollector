using System.Runtime.InteropServices;
using UnityEngine;

public class YandexLoader : ILoader
{
    [DllImport("__Internal")]
    private static extern void LoadExtern();

    public void Load()
    {
        LoadExtern();
    }
}
