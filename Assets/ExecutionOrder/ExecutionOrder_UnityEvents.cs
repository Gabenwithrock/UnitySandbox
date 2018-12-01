using System.Collections;
using UnityEngine;

namespace ExecutionOrder
{
    // Awake -> OnEnable -> Start
	public class ExecutionOrder_UnityEvents : MonoBehaviour {

		private void OnEnable()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents OnEnable");
		}
	
		private void Awake()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents Awake");
		}

		private void Start () 
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents Start");
			StartCoroutine(TestCoroutine());
		}

		public IEnumerator TestCoroutine()
		{
			yield return null;
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents Coroutine after null");

			yield return new WaitForEndOfFrame();
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents Coroutine after WaitForEndOfFrame");
			
			yield return new WaitForFixedUpdate();
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents Coroutine after WaitForFixedUpdate");
		}

		private void LateUpdate()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents LateUpdate");
		}

		private void FixedUpdate()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents FixedUpdate");
		}

		private void OnDisable()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents OnDisable");
		}

		private void OnDestroy()
		{
			ThreadHelper.LogCurThread($"ExecutionOrder_UnityEvents OnDestroy");
		}
	}
}
