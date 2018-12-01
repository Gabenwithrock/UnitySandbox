using System;
using System.ComponentModel;
using System.Timers;
using UnityEngine;

public class Sandbox : MonoBehaviour {
	private Timer t;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] // main thread
	public static void InitBefore()
	{
		ThreadHelper.LogCurThread($"RuntimeInitializeOnLoadMethod.BeforeSceneLoad {DateTime.Now.Millisecond} millisecond");
	}
	
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)] // main thread
	public static void InitAfter()
	{
		ThreadHelper.LogCurThread($"RuntimeInitializeOnLoadMethod.AfterSceneLoad {DateTime.Now.Millisecond} millisecond");
	}

	private void OnEnable()
	{
		ThreadHelper.LogCurThread($"Sandbox OnEnable");
	}
	
	private void Awake()
	{
		ThreadHelper.LogCurThread($"Sandbox Awake");
	}

	private void Start ()
	{
		ThreadHelper.LogCurThread($"Sandbox Start");
		
		ThreadHelper.LogCurThread("UnityMainThread");
		t = new System.Timers.Timer(1000);
		t.Elapsed += (sender, args) =>
		{
			ThreadHelper.LogCurThread("TimerThread"); // NOT IN MAIN THREAD
		};
		t.Start();
		
		var worker = new BackgroundWorker();
		worker.WorkerReportsProgress = true;
		worker.ProgressChanged += (sender, args) =>
		{
			ThreadHelper.LogCurThread($"worker.ProgressChanged => {args.ProgressPercentage/100.0f:P}");
		};
		worker.DoWork += (sender, args) =>
		{
			(sender as BackgroundWorker).ReportProgress(23);
			
			ThreadHelper.LogCurThread("worker.DoWork");
			args.Result = "RESULT!";
		};
		
		worker.RunWorkerCompleted += (sender, args) =>
		{
			ThreadHelper.LogCurThread($"worker.RunWorkerCompleted => {args.Result}"); // NOT IN MAIN THREAD
		};
		worker.RunWorkerAsync();
	}
	
	private void OnDestroy()
	{
		t.Stop();
		t.Close();
	}

	public static void Test()
	{
		//		var ms = new MemoryStream(20);
//		var s = new GZipStream(ms, CompressionMode.Compress);
		
//		System.IO.UnmanagedMemoryStream
//		System.Activator.CreateInstance<Transform>();
//		System.AppDomain.CurrentDomain.CreateInstance("", "");
//		System.AppDomain.CurrentDomain.DomainUnload
//		System.AppDomain.CurrentDomain.UnhandledException
//		System.AppDomain.CurrentDomain.ProcessExit
//		System.Array.TrueForAll()
//		System.Buffer.BlockCopy();
//		System.Convert.FromBase64String()
//		System.Environment.CurrentDirectory
	}
}
