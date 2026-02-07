using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishingPortal : MonoBehaviour
{
    //public bool goNextLevel;
    //public string levelName;

    public GameObject levelComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void LevelComplete()
    //{
    //    levelComplete.SetActive(true);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelComplete.SetActive(true);
            FindAnyObjectByType<GameManager>().GameOver();
            //SceneManagement.instance.NextLevel();
        }
        //else
        //{
        //    GameManager.instance.LoadScene(levelName);
        //}
    }
}
