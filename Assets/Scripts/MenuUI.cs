using UnityEngine;

/// <summary>
/// Handles Menu scene buttons. Uses static instances so buttons work
/// even when the scene's SceneManagement/SaveController objects are destroyed (singleton duplicates).
/// </summary>
public class MenuUI : MonoBehaviour
{
    public void OnNewGameClicked()
    {
        if (SceneManagement.instance != null)
        {
            SceneManagement.instance.NewGame();
        }
    }

    public void OnLoadGameClicked()
    {
        if (SceneManagement.instance != null)
        {
            SceneManagement.instance.LoadGame();
        }
    }

    public void OnExitClicked()
    {
        if (SceneManagement.instance != null)
        {
            SceneManagement.instance.Exit();
        }
    }
}
