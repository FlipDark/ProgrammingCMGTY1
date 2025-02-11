using UnityEngine;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the GameManager to handle score increase
            GameManager.Instance.CollectItem(); // Assuming Singleton pattern is set up in GameManager
            Destroy(gameObject); // Destroy the collectible after it’s collected
        }
    }
}
