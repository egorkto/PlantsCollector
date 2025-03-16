using System;

public static class WorldDataConstructor
{
    public static event Action<WorldData> Construct;

    public static WorldData ConstructData()
    {
        var data = new WorldData();
        Construct?.Invoke(data);
        return data;
    }
}
    
