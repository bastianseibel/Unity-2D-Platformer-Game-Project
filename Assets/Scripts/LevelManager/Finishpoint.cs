using UnityEngine;

public class Finishpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HeroMovement>().enabled = false;
            other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            LevelManager.Instance.LoadLevelCompleteScene();
        }
    }
}