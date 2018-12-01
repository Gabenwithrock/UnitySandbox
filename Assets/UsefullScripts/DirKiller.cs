using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DirKiller {

    [InitializeOnLoadMethod, RuntimeInitializeOnLoadMethod]
    public static void KillAllEmptyFolders()
    {
        var assetsDir = Application.dataPath;
        var childDirectories = Directory.GetDirectories(assetsDir, "*", SearchOption.AllDirectories);

        var removed = new List<string>();

        foreach (var childDirectory in childDirectories)
        {
            var files = Directory.GetFiles(childDirectory);
            if (files.Length == 0 || files.All(x => x.EndsWith(".meta")))
            {
                try
                {
                    Directory.Delete(childDirectory);
                    removed.Add(childDirectory);
                }
                catch (Exception e)
                {
                }     
            }
        }

        if (removed.Count <= 0) return;
        
        Debug.Log($"Empty dirs killed: {removed.Count}\n{string.Join("\n", removed.ToArray())}");
        AssetDatabase.Refresh();
    }
}
