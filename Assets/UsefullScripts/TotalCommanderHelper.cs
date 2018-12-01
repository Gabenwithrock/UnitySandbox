using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TotalCommanderHelper : MonoBehaviour
{
    public const string TotalCommanderPath86 = @"C:\Program Files (x86)\Total Commander\TOTALCMD.EXE";
    public const string TotalCommanderPath64 = @"C:\Program Files\Total Commander\TOTALCMD.EXE";
    private static string totalCommanderPath;

    static TotalCommanderHelper()
    {
        if (File.Exists(TotalCommanderPath64))
            totalCommanderPath = TotalCommanderPath64;
        
        if (File.Exists(TotalCommanderPath86))
            totalCommanderPath = TotalCommanderPath86;
    }
    
    [MenuItem("Assets/Show in TotalCommander")]
    private static void ShowInTotalCommander()
    {
        RunTotalWithArguments();
    }

    //http://www.ghisler.ch/wiki/index.php/Command_line_parameters
    //totalcmd.exe / O / L = c:\ / R = "d:\doc"
    private static void RunTotalWithArguments()
    {
        if (string.IsNullOrEmpty(totalCommanderPath))
        {
            UnityEngine.Debug.LogError("Notepad++ is not installed");
            return;
        }
        
        var process = new Process();
        var startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = totalCommanderPath;
        startInfo.Arguments = string.Format(@"/O /L={0} /R=""{1}""", Application.dataPath, GetSelectionPath());
        startInfo.UseShellExecute = true;
        process.StartInfo = startInfo;
        process.Start();
    }

    private static string GetSelectionPath()
    {
        return Application.dataPath.Substring(0, Application.dataPath.Length - 6) + GetSelectedPathOrFallback();
    }

    public static string GetSelectedPathOrFallback()
    {
        var path = "Assets";

        foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
}
