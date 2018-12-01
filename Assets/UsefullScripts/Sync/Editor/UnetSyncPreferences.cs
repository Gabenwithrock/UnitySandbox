using UnityEditor;

namespace Zarnica
{
    public static class UnetSyncPreferences
    {
        private const string SYNC_FOLDER = "UnetSyncPreferences_SyncFolder";

        public static string SyncFolder
        {
            get { return EditorPrefs.GetString(SYNC_FOLDER); }
            private set { EditorPrefs.SetString(SYNC_FOLDER, value); }
        }

        [PreferenceItem("UNET Sync Preferences")]
        public static void PreferencesGUI()
        {
            EditorGUI.BeginChangeCheck();

            var newSyncFolder = EditorGUILayout.TextField("SyncFolder", SyncFolder);

            if (EditorGUI.EndChangeCheck())
            {
                SyncFolder = newSyncFolder;
            }
        }
    }
}