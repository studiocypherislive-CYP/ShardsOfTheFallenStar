using UnityEngine;
using System.IO;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    public static SaveController instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        
        // Don't auto-load - only load when explicitly requested
        // Auto-load causes issues when scene doesn't have Player yet
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Subscribe to scene loaded event to load game data after scene loads
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called after a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Only auto-load in game scenes (not Menu scene)
        // You can check scene name or build index here
        if (scene.name != "Menu")
        {
            // Small delay to ensure all objects are initialized
            Invoke(nameof(LoadGameDelayed), 0.1f);
        }
    }

    private void LoadGameDelayed()
    {
        LoadGame();
    }

    public void SaveGame()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineConfiner2D confiner = FindAnyObjectByType<CinemachineConfiner2D>();

        if (player == null || confiner == null || confiner.BoundingShape2D == null)
        {
            Debug.LogWarning("Cannot save game: Player or CinemachineConfiner2D not found");
            return;
        }

        SaveData saveData = new SaveData
        {
            playerPosition = player.transform.position,
            mapBoundary = confiner.BoundingShape2D.gameObject.name
        };

        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));
        Debug.Log("Game saved successfully");
    }

    public void LoadGame()
    {
        if (!File.Exists(saveLocation))
        {
            Debug.Log("No save file found. Creating new save.");
            SaveGame();
            return;
        }

        try
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            CinemachineConfiner2D confiner = FindAnyObjectByType<CinemachineConfiner2D>();

            if (player == null)
            {
                Debug.LogWarning("Cannot load game: Player not found in scene");
                return;
            }

            if (confiner == null)
            {
                Debug.LogWarning("Cannot load game: CinemachineConfiner2D not found in scene");
                return;
            }

            // Load player position
            player.transform.position = saveData.playerPosition;

            // Load map boundary
            GameObject boundaryObject = GameObject.Find(saveData.mapBoundary);
            if (boundaryObject != null)
            {
                PolygonCollider2D polygonCollider = boundaryObject.GetComponent<PolygonCollider2D>();
                if (polygonCollider != null)
                {
                    confiner.BoundingShape2D = polygonCollider;
                    Debug.Log("Game loaded successfully");
                }
                else
                {
                    Debug.LogWarning($"Found boundary object '{saveData.mapBoundary}' but it doesn't have PolygonCollider2D");
                }
            }
            else
            {
                Debug.LogWarning($"Boundary object '{saveData.mapBoundary}' not found in scene");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading game: {e.Message}");
        }
    }
}
