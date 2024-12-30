using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI coinText;

    // * Singleton pattern

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

    // * Start method to update coin text when the game starts
    void Start()
    {
        if (SaveLoadManager.Instance != null)
        {
            UpdateCoinText();
        }
    }

    // * Add coin to total and update UI
    public void addCoin(Coin coin)
    {
        if (SaveLoadManager.Instance != null)
        {
            SaveLoadManager.Instance.AddCoins(coin.value);
            UpdateCoinText();
        }

        Debug.Log($"Coin collected! Value: {coin.value}, Total: {SaveLoadManager.Instance.totalCoins}");
    }

    // * Update coin text with current coin count
    public void UpdateCoinText()
    {
        if (coinText != null && SaveLoadManager.Instance != null)
        {
            coinText.text = $"x {SaveLoadManager.Instance.totalCoins:00000}";
        }
    }
}