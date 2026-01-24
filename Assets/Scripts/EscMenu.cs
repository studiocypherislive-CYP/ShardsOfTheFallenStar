using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    public GameObject escMenuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        escMenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            escMenuCanvas.SetActive(!escMenuCanvas.activeSelf);
        }
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
