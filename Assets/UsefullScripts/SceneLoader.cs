using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class SceneLoader
{
    public static bool GoBack
    {
        get
        {
            bool val;
            bool.TryParse(EditorPrefs.GetString("goBack"), out val);
            return val;
        }
        set { EditorPrefs.SetString("goBack", value.ToString()); }
    }

    public static string PrevScene
    {
        get
        {
            var res = EditorPrefs.GetString("prev");
            return res;
        }
        set
        {
            EditorPrefs.SetString("prev", value);
        }
    }

    static SceneLoader()
    {
        EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
    }

    [MenuItem("Tools/Scenes/Run From Begining")]
    public static void LoadFromBegining()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();

        PrevScene = EditorSceneManager.GetActiveScene().path;
        GoBack = true;

        EditorApplication.isPlaying = true;
        EditorApplication.playmodeStateChanged += PlaymodeStateChanged;
        var firstScene = EditorSceneManager.GetSceneByBuildIndex(0);
        EditorSceneManager.OpenScene(firstScene.path);
    }

    private static void PlaymodeStateChanged()
    {
        if (EditorApplication.isPlayingOrWillChangePlaymode == false && EditorApplication.isPlaying == false && GoBack)
        {
            GoBack = false;
            EditorSceneManager.OpenScene(PrevScene);
        }
    }
}
