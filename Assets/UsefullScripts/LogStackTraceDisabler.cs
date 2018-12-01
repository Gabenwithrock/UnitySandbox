using UnityEngine;

public class LogStackTraceDisabler {

#if UNITY_ANDROID && ! UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
    }
#endif
}
