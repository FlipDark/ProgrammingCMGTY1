using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    public UnityEvent PlayerDeath;
    [SerializeField] TMP_Text livesDisplay;
    [SerializeField] private float lives = 3;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") Damage();
    }

    private void Damage()
    {
        if (lives > 0)
        {
            lives--;
            livesDisplay.text = lives.ToString();
        }
        
        if (lives == 0)
        {
            PlayerDeath?.Invoke();
        }
    }
}
