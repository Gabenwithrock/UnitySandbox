using System.Collections.Generic;

using NUnit.Framework;
using UnityEngine;

namespace DTValidator {
	public static class ValidationTests
	{
		private const bool earlyExitOnError = false;
		
		[Test]
		public static void ValidateSavedScriptableObjects() {
			IList<IValidationError> errors = ValidationUtil.ValidateAllSavedScriptableObjects(earlyExitOnError);
			LogErrors("ValidateSavedScriptableObjects", errors);
			Assert.That(errors, Is.Empty);
		}

		[Test]
		public static void ValidateGameObjectsInResources() {
			IList<IValidationError> errors = ValidationUtil.ValidateAllGameObjectsInResources(earlyExitOnError);
			LogErrors("ValidateGameObjectsInResources", errors);
			Assert.That(errors, Is.Empty);
		}

		[Test]
		public static void ValidateSavedScenes() {
			IList<IValidationError> errors = ValidationUtil.ValidateAllGameObjectsInSavedScenes(earlyExitOnError);
			LogErrors("ValidateSavedScenes", errors);
			Assert.That(errors, Is.Empty);
		}
		
		private static void LogErrors(string header, IList<IValidationError> errors)
		{
			if (errors == null || errors.Count == 0)
				return;

			var errorsLines = string.Join("\n", errors);
			var res = header + "\n" + errorsLines;
			Debug.LogError(res);
		}
	}
}