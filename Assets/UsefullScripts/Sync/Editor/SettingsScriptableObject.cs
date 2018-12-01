using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Gabenwithrock
{
    public class SettingsScriptableObject<T>: ScriptableObject where T : ScriptableObject
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    var type = typeof(T);
                    var settings = FinAllSettings();

                    if (settings.Count == 0)
                    {
                        var allAssets = Resources.FindObjectsOfTypeAll<TextAsset>();
                        var scriptAsset = allAssets.FirstOrDefault(x => x.name == type.Name);
                        var path = AssetDatabase.GetAssetPath(scriptAsset);
                        var dir = path.Replace(type.Name + ".cs", "");
                        var so = (T) CreateInstance(type);
                        var targetPath = dir + type.Name + ".asset";
                        AssetDatabase.CreateAsset(so, targetPath);
                        Debug.Log(string.Format("{0} was created at targetPath", targetPath), so);

                        instance = so;
                    }
                    else
                    {
                        instance = settings.FirstOrDefault();
                        if (settings.Count > 1)
                        {
                            var path = AssetDatabase.GetAssetPath(instance);
                            var formatString = "There are {0} setting files for {1}. Unused setting files should be removed.\r\nSelected {2}\r\nAll available setting files:\r\n";
                            for (int i = 0; i < settings.Count; i++)
                            {
                                var otherPath = AssetDatabase.GetAssetPath(settings[i]);
                                formatString += otherPath + "\r\n";
                            }

                            Debug.LogError(string.Format(formatString, settings.Count, type.FullName, path), instance);
                        }
                    }
                }

                return instance;
            }
        }

        private static List<T> FinAllSettings()
        {
            List<T> settings = null;
            var allPathes = AssetDatabase.GetAllAssetPaths();
            var allAssets = new List<Object>();
            foreach (var path in allPathes)
            {
                var obj = AssetDatabase.LoadMainAssetAtPath(path);
                if (obj != null)
                    allAssets.Add(obj);
            }

            settings = Resources.FindObjectsOfTypeAll<T>().ToList();

           //foreach (var asset in allAssets)
           //{
           //    var isAsset = AssetDatabase.Contains(asset);
           //    if (isAsset )
           //        Resources.UnloadAsset(asset);
           //}
            Resources.UnloadUnusedAssets();
            GC.Collect();

            return settings;
        }
    }
}