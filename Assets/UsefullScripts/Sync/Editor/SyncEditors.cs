using System;
using System.IO;
using System.Linq;
using System.Text;
using Networking.Editor;
using UnityEditor;
using UnityEngine;
using Zarnica;

public class SyncEditors : AssetPostprocessor
{
    public const string SyncName = " (Sync)";
    private const string MENU_ROOT = "Tools/Project Sync/";
    
    public static string MainProjectRoot
    {
        get
        {
            var res = Directory.GetParent(Application.dataPath).FullName.Replace(SyncName, "");
            return res;
        }
    }

    public static string SyncProjectRoot
    {
        get
        {
            var res = Directory.GetParent(Application.dataPath).FullName.Replace(SyncName, "") + SyncName;
            return res;
        }
    }

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        if (!SyncEditorsSettings.Instance.Sync)
            return;

        if (Application.dataPath.Contains(SyncName))
            return;

        var sb = new StringBuilder();
        var beforeAssets = Application.dataPath.Substring(0, Application.dataPath.Length - 6);

        importedAssets = importedAssets.Select(x =>
        {
            x = beforeAssets + x;
            x = x.Replace("/", "\\");
            return x;
        }).ToArray();
        CopyImported(importedAssets, sb);
        importedAssets = importedAssets.Select(x => x + ".meta").ToArray();
        CopyImported(importedAssets, sb);

        deletedAssets = deletedAssets.Select(x => beforeAssets + x).ToArray();
        DeleteDeleted(deletedAssets, sb);
        deletedAssets = deletedAssets.Select(x => x + ".meta").ToArray();
        DeleteDeleted(deletedAssets, sb);

        movedAssets = movedAssets.Select(x => beforeAssets + x).ToArray();
        movedFromAssetPaths = movedFromAssetPaths.Select(x => beforeAssets + x).ToArray();
        Move(movedAssets, movedFromAssetPaths, sb);
        movedAssets = movedAssets.Select(x => x + ".meta").ToArray();
        movedFromAssetPaths = movedFromAssetPaths.Select(x => x + ".meta").ToArray();
        Move(movedAssets, movedFromAssetPaths, sb);

        if (SyncEditorsSettings.Instance.PrintLog)
            Debug.Log(sb.ToString());
    }

    private static void Move(string[] movedAssets, string[] movedFromAssetPaths, StringBuilder sb)
    {
        for (int i = 0; i < movedAssets.Length; i++)
        {
            try
            {
                var newPath = movedAssets[i];
                var deletedPath = movedFromAssetPaths[i];
                var destNewPath = Application.dataPath.Contains(SyncName)
                    ? newPath.Replace(SyncProjectRoot, MainProjectRoot)
                    : newPath.Replace(MainProjectRoot, SyncProjectRoot);

                var deletedNewPath = Application.dataPath.Contains(SyncName)
                    ? deletedPath.Replace(SyncProjectRoot, MainProjectRoot)
                    : deletedPath.Replace(MainProjectRoot, SyncProjectRoot);

                File.Delete(deletedNewPath);
                File.Copy(newPath, destNewPath, true);

                sb.AppendLine(deletedNewPath + " moved to " + deletedNewPath);
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }
        }
    }

    private static void DeleteDeleted(string[] deletedAssets, StringBuilder sb)
    {
        foreach (string fullPath in deletedAssets)
        {
            try
            {
                var destPath = Application.dataPath.Contains(SyncName)
                    ? fullPath.Replace(SyncProjectRoot, MainProjectRoot)
                    : fullPath.Replace(MainProjectRoot, SyncProjectRoot);
                if (Directory.Exists(destPath))
                    Directory.Delete(destPath);
                else
                    File.Delete(destPath);
                sb.AppendLine(destPath + " deleted");
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }
        }
    }

    private static void CopyImported(string[] importedAssets, StringBuilder sb)
    {
        foreach (string fullPath in importedAssets)
        {
            try
            {
                if (!File.Exists(fullPath))
                    continue;

                var destPath = Application.dataPath.Contains(SyncName)
                    ? fullPath.Replace(SyncProjectRoot, MainProjectRoot)
                    : fullPath.Replace(MainProjectRoot, SyncProjectRoot);
                if (Directory.Exists(fullPath))
                    Directory.CreateDirectory(destPath);
                else
                {
                    if (File.Exists(destPath))
                        File.Delete(destPath);
                    File.Copy(fullPath, destPath, true);
                }

                sb.AppendLine(destPath + " updated");
            }
            catch (Exception e)
            {
                sb.AppendLine(e.Message);
            }
        }
    }

    public static string AssetsRoot
    {
        get { return Application.dataPath.Replace(SyncName,""); }
    }

    public static string SyncAssetsRoot
    {
        get { return Path.Combine(SyncProjectRoot, "Assets"); }
    }

    public static string ProjectSyncFolder
    {
        get { return Path.Combine(MainProjectRoot, UnetSyncPreferences.SyncFolder); }
    }

    public static string SyncProjectSyncFolder
    {
        get { return Path.Combine(SyncProjectRoot, UnetSyncPreferences.SyncFolder); }
    }

    [MenuItem(MENU_ROOT + "Server to Client (Assets)")]
    public static void SendAssetsStoC_Project()
    {
        DeleteAndCopy(AssetsRoot, SyncAssetsRoot);
    }

    [MenuItem(MENU_ROOT + "Server to Client (Folder)")]
    public static void SendAssetsStoC_Folder()
    {
        DeleteAndCopy(ProjectSyncFolder, SyncProjectSyncFolder);
    }

    private static void DeleteAndCopy(string src, string dst)
    {
        if (Directory.Exists(dst))
        {
            try
            {
                Directory.Delete(dst, true);
            }
            catch (Exception e){}
        }

        Directory.CreateDirectory(dst);
        CopyDirectory(src, dst);
    }

    private static void CopyDirectory(string sourcePath, string destinationPath)
    {
        foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        {
            if (IsIgnoredDir(dirPath))
                continue;
            Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
        }

        //Copy all the files & Replaces any files with the same name
        var allFiles = Directory.GetFiles(sourcePath, "*.*",
            SearchOption.AllDirectories);
        var totalCount = allFiles.Length;

        EditorUtility.DisplayProgressBar("Copy", "In Progress", 0);
        int i = 0;
        foreach (string newPath in allFiles)
        {
            if (IsIgnoredDir(newPath))
                continue;
            try
            {
                File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
            }
            catch (Exception e)
            {
                Debug.LogError(e.ToString());
            }
            
            i++;
            var isCancelled = EditorUtility.DisplayCancelableProgressBar("Copy", string.Format("In Progress: {0}/{1}", i, totalCount), (float)i / totalCount);
            if (isCancelled)
                break;
        }
        
        EditorUtility.ClearProgressBar();

        EditorUtility.DisplayDialog("UNET Sync",
            $"Sync Done: {i}/{totalCount}"
            + "\nSource: " + sourcePath
            + "\nDest: " + destinationPath
            , "OK");
    }

    private static bool IsIgnoredDir(string dirPath)
    {
        var res =
                dirPath.Contains(".git")
                || dirPath.Contains("Temp")
                || dirPath.Contains("Library")
            ;
        return res;
    }
}
