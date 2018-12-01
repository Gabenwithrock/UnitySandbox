using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace UsefullScripts
{
    public class ProjectReferencesRemover: AssetPostprocessor {
        private static readonly string[] references =
        {
            "Boo.Lang", "UnityScript", "UnityScript.Lang"
        };

        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Editor/Mono/AssetPostprocessor.cs
        // public static void CallOnGeneratedCSProjectFiles() // check in 2018.2.0 or higher
        // public static string CallOnGeneratedCSProject(string path, string content) // check in 2018.2.0 or higher
        // public static bool OnPreGeneratingCSProjectFiles() // BUG: if used, doesn't add new scripts to project
        [DidReloadScripts, InitializeOnLoadMethod, RuntimeInitializeOnLoadMethod]
        public static void OnScriptsReloaded()
        {
            var curDir = Directory.GetCurrentDirectory();
            var projects = Directory.GetFiles(curDir, "*.csproj");
            foreach (var project in projects)
                RemoveReferences(project, references);
        }

        private static void RemoveReferences(string projectPath, string[] strings)
        {
            var projectFileLines = File.ReadAllLines(projectPath);
            var linesList = new List<string>(projectFileLines);
        
            foreach (var reference in strings)
                RemoveReference(linesList, reference);

            File.WriteAllLines(projectPath, linesList);
        }

        private static void RemoveReference(List<string> lines, string reference)
        {
            var targetIndex = GetIndex(lines, reference);
            if (targetIndex < 0)
                return;
        
            lines.RemoveRange(targetIndex, 3);
        }

        private static int GetIndex(List<string> lines, string reference)
        {
            var targetLine = $"<Reference Include=\"{reference}\">";
            var res = -1;
        
            for (var i = 0; i < lines.Count; i++)
            {
                if (!lines[i].Contains(targetLine)) 
                    continue;
            
                res = i;
                break;
            }

            return res;
        }
    }
}