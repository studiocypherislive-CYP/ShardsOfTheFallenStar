using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
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
}
