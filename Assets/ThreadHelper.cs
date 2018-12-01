using System.Threading;
using UnityEngine;

public static class ThreadHelper {
	public static void LogCurThread(string header = "")
	{
		var curThread = Thread.CurrentThread;
		Debug.Log($"{header}\n" 
		          + $"ThreadName = \"{curThread.Name}\" "
		          + $"ManagedId = {curThread.ManagedThreadId} "
		          + $"IsBackground = {curThread.IsBackground} "
		          + $"IsThreadPoolThread = {curThread.IsThreadPoolThread} "
		          + $"Priority = {curThread.Priority.ToString()} "
		          );
	}

//	public static Thread UnityMainThread;
//	public static ExecutionContext UnityMainContext;
//	
//	[InitializeOnLoadMethod]
//	public static void Init()
//	{
//		UnityMainThread = Thread.CurrentThread;
//		UnityMainContext = ExecutionContext.Capture();
//		
//		if (UnityMainThread == null)
//			return;
//
//		Debug.Log($"START TEST THREAD. {UnityMainContext}");
//		var thread = new Thread(() =>
//		{
//			Debug.Log($"BEFORE ExecutionContext.Run. UnityContext: {UnityMainContext}");
//			try
//			{
//				ExecutionContext.Run(UnityMainContext, state =>
//				{
//					try
//					{
//						Debug.Log("TRY UNITY API ExecutionContext.Run");
//						var go = GameObject.Find("Main Camera");
//						go.transform.position = Vector3.one;
//					}
//					catch (Exception e)
//					{
//						Debug.LogError(e.ToString());
//					}
//				}, null);
//			}
//			catch (Exception e)
//			{
//				Debug.LogError(e.ToString());
//			}
//		});
//		thread.Start();
//	}
	
}
	