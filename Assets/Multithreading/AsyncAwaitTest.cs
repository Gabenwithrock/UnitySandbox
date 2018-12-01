using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AsyncAwaitTest : MonoBehaviour
{
    private void Start()
    {
        TestAsync();
        ThreadHelper.LogCurThread($"After async test {DateTime.Now}"); // 2 in MainThread
    }

    public async void TestAsync()
    {
        ThreadHelper.LogCurThread($"TestAsync: Begin {DateTime.Now}"); // 1 in MainThread
        
        int answer = await Task.Run(() => // runs in background worker thread
        {
            ThreadHelper.LogCurThread($"Await task begin {DateTime.Now}"); // 3 in worker thread
            Thread.Sleep(1000);
            ThreadHelper.LogCurThread($"Awaited task done {DateTime.Now}"); // 4 in worker thread
            return 10;
        });
        
        ThreadHelper.LogCurThread($"TestAsync: end {DateTime.Now}"); // 5 in MainThread
    }
}