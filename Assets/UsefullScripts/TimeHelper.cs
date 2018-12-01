using UnityEngine;

namespace Gabenwithrock.Helpers
{
    public class TimeHelper : MonoBehaviour
    {
        private Rect rect;

        [RuntimeInitializeOnLoadMethod()]
        private static void InitializeOnLoadRuntime()

        {
            var go = new GameObject("TimeHelper", typeof(TimeHelper));
            go.hideFlags = HideFlags.HideAndDontSave;
        }

        private void Awake()
        {
            rect = new Rect(Screen.width - 30, 0, 30, 20);
        }

        private void Update()
        {
           if (Input.GetKeyDown(KeyCode.LeftBracket))
               Time.timeScale = Mathf.Clamp(Time.timeScale * 0.5f, 1, 100);
           
           if (Input.GetKeyDown(KeyCode.RightBracket))
               Time.timeScale = Mathf.Clamp(Time.timeScale * 2, 1, 64);
           
           if (Input.GetKeyDown(KeyCode.Quote))
               Time.timeScale = 1;
           
           if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Pause))
               Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }

        private void OnGUI()
        {
            var ts = Time.timeScale;
            if (ts != 1)
            {
                GUI.Label(rect, "x" + (int)ts);
            }
        }
    }
}