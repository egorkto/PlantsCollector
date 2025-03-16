using System.IO;
 using UnityEngine;
 
 public class LocalRepositorySaver : ISaver
 {
     private readonly string _path;
 
     public LocalRepositorySaver(string path)
     {
         _path = path;
     }
 
     public void Save(WorldData data)
     {
         var json = JsonUtility.ToJson(data);
         File.WriteAllText(_path, json);
     }
 }