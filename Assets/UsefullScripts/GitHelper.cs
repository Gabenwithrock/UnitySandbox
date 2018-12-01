using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Gabenwithrock
{
    public class GitHelper
    {
        [MenuItem("Assets/TortoiseGIT/Log")]
        private static void TortoiseLog()
        {
            RunTortoiseCommand("log");
        }

        [MenuItem("Assets/TortoiseGIT/Commit")]
        private static void TortoiseCommit()
        {
            RunTortoiseCommand("commit");
        }

        [MenuItem("Assets/TortoiseGIT/Pull")]
        private static void TortoisePull()
        {
            RunTortoiseCommand("pull");
        }

        [MenuItem("Assets/TortoiseGIT/Blame")]
        private static void TortoiseBlame()
        {
            RunTortoiseCommand("blame");
        }

        [MenuItem("Assets/TortoiseGIT/Push")]
        private static void TortoisePush()
        {
            RunTortoiseCommand("push");
        }

        private static void RunTortoiseCommand(string gitCommand)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = @"cmd";
            startInfo.Arguments = "/c " + GetGitCommand(gitCommand);
            startInfo.UseShellExecute = true;
            process.StartInfo = startInfo;
            process.Start();
        }

        private static string GetGitCommand(string gitCommand)
        {
            var path = GetSelectedPathOrFallback();

            if (path == Application.dataPath) // If Assets - Commit root
                path = Directory.GetParent(path).FullName;

            var res = string.Format(@"TortoiseGitProc /command:{0} /path:""{1}""", gitCommand, path);
            //var res = string.Format(@"git /command:{0} /path:""{1}""", gitCommand, path); // TODO: git-bash
            return res;
        }

        public static string GetSelectedPathOrFallback()
        {
            string path = Application.dataPath;
            var dataPath = Application.dataPath;
            dataPath = dataPath.Substring(0, dataPath.Length - 6);
            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = dataPath + AssetDatabase.GetAssetPath(obj);
                if (!(File.Exists(path) || Directory.Exists(path)))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }
}