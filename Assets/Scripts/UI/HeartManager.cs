using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    // * References to the heart images and sprites
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    
    // * Reference to the player health component
    private HeroHealth playerHealth;

    // * Start the game and get the player health component
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroHealth>();
        UpdateHearts();
    }

    // * Update the hearts based on the player's health
    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}