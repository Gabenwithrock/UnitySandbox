using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace SceneValidation.Editor
{
	public class SceneValidationTests
	{
		[Test]
		public void NoMissingPrefabsInScenes()
		{
			var noMissingPrefabsInScenes = true;
			var sb = new StringBuilder();
			sb.AppendLine("SceneValidationTests:");
			
			foreach (var scene in GetSavedScenes())
			{
				var rootObjects = scene.GetRootGameObjects();
				var count = rootObjects.Count(x => x.name == "Missing Prefab");
				if (count <= 0) 
					continue;
				
				noMissingPrefabsInScenes = false;
				sb.AppendLine($"{scene.name} has {count} Missing Prefab(s)");
			}

			if (!noMissingPrefabsInScenes)
				Debug.LogError(sb.ToString());
			
			Assert.IsTrue(noMissingPrefabsInScenes);
		}

// BUG: finds canvas on scene without canvas
//		[Test]
//		public void CheckEventSystemIfCanvasExist()
//		{
//			var sb = new StringBuilder();
//			sb.AppendLine("CheckEventSystemIfCanvasExist:");
//
//			var allScenesValid = true;
//			foreach (var scene in GetSavedScenes())
//			{
//				var hasCanvas = false;
//				var rootObjects = scene.GetRootGameObjects();
//				foreach (var gameObject in rootObjects)
//				{
//					var canvas = gameObject.GetComponentInChildren<Canvas>();
//					if (canvas == null)
//						continue;
//
//					hasCanvas = true;
//					break;
//				}
//
//				if (hasCanvas)
//					continue;
//
//				var hasEventSystem = false;
//				foreach (var gameObject in rootObjects)
//				{
//					var eventSystem = gameObject.GetComponentInChildren<EventSystem>();
//					if (eventSystem == null || !eventSystem.gameObject.activeInHierarchy)
//						continue;
//
//					hasEventSystem = true;
//					break;
//				}
//
//				if (!hasEventSystem)
//				{
//					allScenesValid = false;
//					sb.AppendLine($"{scene.name} has Canvas but no active EventSystem");
//				}
//			}
//			
//			if (!allScenesValid)
//				Debug.LogError(sb.ToString());
//			
//			Assert.IsTrue(allScenesValid);
//		}
		
		private static IEnumerable<Scene> GetSavedScenes() {
			string[] guids = AssetDatabase.FindAssets("t:Scene");
			foreach (string guid in guids)
			{
				var scene = EditorSceneManager.OpenScene(AssetDatabase.GUIDToAssetPath(guid), OpenSceneMode.Additive);
				yield return scene;
				EditorSceneManager.CloseScene(scene, true);
			}
		}
	}
}
