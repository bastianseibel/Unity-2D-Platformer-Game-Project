using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    // * Coin tracking and UI
    public int coinCount = 0;
    public TMP_Text coinText;

    // * Initialize coin display
    void Start()
    {
        UpdateCoinText();
    }

    // * Add coin value and update display
    public void addCoin(Coin coin)
    {
        coinCount += coin.value;
        UpdateCoinText();
    }

    // * Update UI text with current coin count
    private void UpdateCoinText()
    {
        coinText.text = $"x {coinCount:00000}";
    }
}