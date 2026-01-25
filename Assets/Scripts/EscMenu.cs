using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject escMenuCanvas;
    private bool isPaused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        escMenuCanvas.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void SaveGame()
    {
        if (SaveController.instance != null)
        {
            SaveController.instance.SaveGame();
        }
    }

    public void LoadGame()
    {
        if (SaveController.instance != null)
        {
            SaveController.instance.LoadGame();
            // Close the menu after loading
            escMenuCanvas.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
