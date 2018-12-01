using UnityEditor;

namespace DTValidator {
	public static class DTValidatorPreferences {
		public static bool ValidateSceneAutomatically {
			get { return EditorPrefs.GetBool("DTValidatorPreferences::ValidateSceneAutomatically", defaultValue: false); }
			set { EditorPrefs.SetBool("DTValidatorPreferences::ValidateSceneAutomatically", value); }
		}

//		[PreferenceItem("DTValidator")]
//		public static void PreferencesGUI() {
//			ValidateSceneAutomatically = EditorGUILayout.Toggle("Validate Scene Automatically", ValidateSceneAutomatically);
//		}

		[MenuItem("Tools/DTValidator/Enable auto scene validation", priority=100)]
		public static void EnableAutoSceneValidation()
		{
			ValidateSceneAutomatically = true;
			ReimportRandomScript();
		}
		
		[MenuItem("Tools/DTValidator/Disable auto scene validation", priority=100)]
		public static void DisableAutoSceneValidation()
		{
			ValidateSceneAutomatically = false;
			ReimportRandomScript();
		}
		
		private static void ReimportRandomScript () {
			var scripts = AssetDatabase.FindAssets("t:MonoScript");
			if (scripts == null || scripts.Length == 0)
				return;

			var scriptPath = AssetDatabase.GUIDToAssetPath(scripts[0]);
			AssetDatabase.ImportAsset(scriptPath);
		}
	}
}
