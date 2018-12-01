using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper: MonoBehaviour {
    [RuntimeInitializeOnLoadMethod()]
    private static void InitializeOnLoadRuntime()
    {
        var go = new GameObject("SceneHelper", typeof(SceneHelper));
        go.hideFlags = HideFlags.HideAndDontSave;
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.F2))
            SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.F3))
            SceneManager.LoadScene(2);
        if (Input.GetKeyDown(KeyCode.F4))
            SceneManager.LoadScene(3);

        if (Input.GetKeyDown(KeyCode.F5))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if (Input.GetKeyDown(KeyCode.F12))
            Application.Quit();
	}
}
