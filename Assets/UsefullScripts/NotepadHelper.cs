using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class NotepadHelper : MonoBehaviour
{
    public const string NotepadPath86 = @"C:\Program Files (x86)\Notepad++\notepad++.exe";
    public const string NotepadPath64 = @"C:\Program Files\Notepad++\notepad++.exe";
    private static string notepadPath;

    static NotepadHelper()
    {
        if (File.Exists(NotepadPath64))
            notepadPath = NotepadPath64;
        
        if (File.Exists(NotepadPath86))
            notepadPath = NotepadPath86;
    }
    
    
    [MenuItem("Assets/Show in Notepad++")]
    private static void ShowInNotepad()
    {
        RunNotepadWithArguments();
    }

    //http://docs.notepad-plus-plus.org/index.php/Command_Line_Switches
    private static void RunNotepadWithArguments()
    {
        if (string.IsNullOrEmpty(notepadPath))
        {
            Debug.LogError("Notepad++ is not installed");
            return;
        }

        var process = new Process();
        var startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.FileName = notepadPath;
        startInfo.Arguments = GetSelectionPath();
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
                return path;
            }
        }
        
        return path;
    }
}
