using System.IO;
 using UnityEngine;
 
 public class LocalRepositoryLoader : ILoader
 {
     private readonly string _path;
     
     public LocalRepositoryLoader(string path)
     {
         _path = path;
     }
 
     public WorldData Load()
     {
         if(File.Exists(_path))
         {
             var json = File.ReadAllText(_path);
             var data = JsonUtility.FromJson<WorldData>(json);
             return data;
         }
         return null;
     }
 }