using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public UnityEvent PlayerDeath; // Unity event to trigger player death (like scene switching)
    private GameManager gameManager; // Reference to the GameManager

    private void Start()
    {
        // Get the GameManager reference
        gameManager = GameManager.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an obstacle
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Damage(); // Call the Damage function when colliding with an obstacle
        }
    }

    private void Damage()
    {
        if (gameManager != null)
        {
            gameManager.LoseLife(); // Call LoseLife in GameManager to decrease lives and respawn
        }

        // If lives reach 0 after losing a life, trigger the UnityEvent to switch scene
        if (gameManager.CurrentLives <= 0)
        {
            Debug.Log("Player Death Triggered");
            PlayerDeath?.Invoke(); // Invoke the UnityEvent for scene switching or game over logic
        }
    }
}
