using UnityEngine;
using UnityEngine.SceneManagement;


// * Finishpoint class to handle level completion
public class Finishpoint : MonoBehaviour
{
    // * Handle level completion when the player enters the finishpoint
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            SaveLoadManager.Instance.UnlockLevel(currentIndex);

            other.GetComponent<HeroMovement>().enabled = false;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            LevelManager.Instance.LoadLevelCompleteScene();
        }
    }
}