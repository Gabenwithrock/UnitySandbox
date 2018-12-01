using System;
using System.Runtime.ExceptionServices;
using UnityEngine;

namespace ExecutionOrder
{
    public class ExecutionOrder_Domain : MonoBehaviour
    {
        private void Awake()
        {
            AppDomain.CurrentDomain.DomainUnload += OnDomainUnload;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            
//            throw new Exception("TestException Awake");
        }

        private void Start()
        {
            throw new Exception("TestException Start");
        }
        
        // not called on app exit PC
        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ThreadHelper.LogCurThread($"ExecutionOrder_Domain CurrentDomainOnUnhandledException");
        }

        // WORKS on windows PC, not works on Android
        private static void CurrentDomainOnProcessExit(object sender, EventArgs e) // is called on app exit PC: Alt+F4
        {
            ThreadHelper.LogCurThread($"ExecutionOrder_Domain CurrentDomainOnProcessExit");
        }

        // not called on app exit PC
        private static void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            ThreadHelper.LogCurThread($"ExecutionOrder_Domain CurrentDomainOnFirstChanceException");
        }

        // not called on app exit PC: Alt+F4
        private static void OnDomainUnload(object sender, EventArgs e)
        {
            ThreadHelper.LogCurThread($"ExecutionOrder_Domain OnDomainUnload");
        }
    }
}