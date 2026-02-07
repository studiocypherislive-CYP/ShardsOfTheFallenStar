using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement instance;

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Exit()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    /// <summary>
    /// Start a new game from the beginning (spawn point, no saved data loaded).
    /// </summary>
    public void NewGame()
    {
        SaveController.RequestNewGame();
        SceneManager.LoadScene("SampleScene");
    }

    /// <summary>
    /// Load the game scene and restore the last saved position/data.
    /// </summary>
    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
