using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI highscoreText; 
    [SerializeField] private int level = 0; // Aktuelle Level

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SaveLoadManager.Instance != null)
        {
            UpdateCoinText();
            UpdateHighscoreText(); 
        }
    }

    public void addCoin(Coin coin)
    {
        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.AddCoins(coin.value);
            UpdateCoinText();
            SaveHighscore(level, SaveLoadManager.Instance.totalCoins); // Hier Level 
        }

        Debug.Log($"Coin collected! Value: {coin.value}, Total: {SaveLoadManager.Instance.totalCoins}");
    }

    public void UpdateCoinText()
    {
        if (coinText != null && SaveLoadManager.Instance != null)
        {
            coinText.text = $"x {SaveLoadManager.Instance.totalCoins:00000}";
        }
    }

    // * Save and update high score
    public void SaveHighscore(int level, int score)
    {
        int savedHighscore = PlayerPrefs.GetInt($"Highscore{level}", 0);
        if (score > savedHighscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            UpdateHighscoreText(); 
        }
    }

    // * Update the high score text
    public void UpdateHighscoreText()
    {
        if (highscoreText != null)
        {
            int savedHighscore = PlayerPrefs.GetInt("Highscore", 0);
            highscoreText.text = $"High Score: {savedHighscore}";
        }
    }
}
