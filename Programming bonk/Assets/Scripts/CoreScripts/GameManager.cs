using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance

    [Header("Game Settings")]
    public int maxLives = 3; // The maximum number of lives
    private int currentLives; // The player's current lives

    public int CurrentLives { get { return currentLives; } }

    [Header("Road Settings")]
    public GameObject roadPrefab; // Road segment prefab
    public int numRoads = 5; // Number of roads in scene
    public float roadLength = 10f; // Length of each road segment

    [Header("Obstacles & Collectibles")]
    public GameObject[] obstaclePrefabs; // Array of obstacle prefabs
    public GameObject[] collectiblePrefabs; // Array of collectible prefabs

    [Header("UI Elements")]
    public TMP_Text scoreText; // Score text (Drag from UI)
    public TMP_Text livesText; // Lives text (Drag from UI)

    private Queue<GameObject> activeRoads = new Queue<GameObject>(); // Stores active roads
    private float spawnOffset = 0f; // Position for next road
    private static int score = 0; // Player score


    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one GameManager exists
        }

        currentLives = maxLives; // Set lives to max at the start
        UpdateScoreUI();
        UpdateLivesUI();
    }

    void Start()
    {
        // Spawn initial road segments
        for (int i = 0; i < numRoads; i++)
        {
            SpawnRoad();
        }
    }

    void Update()
    {
        // Spawn new roads when the player moves forward
        if (FindObjectOfType<Movement>().transform.position.z >= spawnOffset - (numRoads * roadLength))
        {
            SpawnRoad();
        }
    }

    public void LoseLife()
    {
        currentLives--; // Decrease lives
        RespawnPlayer(); // Respawn the player every time lives decrease

        if (currentLives <= 0)
        {
            // Player has no lives left, handle game over (optional)
            Debug.Log("Game Over");
            // You can add more logic for game over here, like showing a Game Over screen
        }

        UpdateLivesUI(); // Update the UI (now done in GameManager only)
    }

    void RespawnPlayer()
    {
        // Reset the player's position to the beginning of the map
        var player = FindObjectOfType<Movement>();
        if (player != null)
        {
            player.transform.position = new Vector3(0, 0, 0); // Adjust this depending on your game layout
            player.ResetPlayerMovement(); // Call the method to reset movement state
        }

    }

    void SpawnRoad()
    {
        // Instantiate a new road at the next position
        GameObject newRoad = Instantiate(roadPrefab, new Vector3(0, 0, spawnOffset), Quaternion.Euler(0, 90, 0));
        newRoad.name = "Road_" + spawnOffset;
        activeRoads.Enqueue(newRoad);

        // Add obstacles and collectibles
        SpawnObstaclesAndCollectibles(newRoad);

        // Move spawn position forward
        spawnOffset += roadLength;
    }

    void SpawnObstaclesAndCollectibles(GameObject road)
    {
        int numObstacles = Random.Range(1, 3); // Random obstacles per road
        int numCollectibles = Random.Range(1, 2); // Random collectibles per road

        for (int i = 0; i < numObstacles; i++)
        {
            GameObject obstacle = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 pos = new Vector3(Random.Range(-2.5f, 2.5f), 0.5f, road.transform.position.z + Random.Range(1f, roadLength - 1f));
            Instantiate(obstacle, pos, Quaternion.identity);
        }

        for (int i = 0; i < numCollectibles; i++)
        {
            GameObject collectible = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
            Vector3 pos = new Vector3(Random.Range(-2.5f, 2.5f), 1f, road.transform.position.z + Random.Range(1f, roadLength - 1f));
            Instantiate(collectible, pos, Quaternion.identity);
        }
    }

    public void CollectItem()
    {
        score += 10; // Increase score by 10
        UpdateScoreUI(); // Update the UI to reflect the new score
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}
