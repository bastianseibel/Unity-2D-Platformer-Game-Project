using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    public TMP_Text coinText;

    public void addCoin(Coin coin)
    {
        coinCount += coin.value;
        UpdateCoinText();
    }

    void Start()
    {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = $"x {coinCount:00000}";
    }
}
