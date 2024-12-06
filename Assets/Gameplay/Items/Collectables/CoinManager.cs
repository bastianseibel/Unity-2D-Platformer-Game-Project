using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class CoinManager : MonoBehaviour
{
    public int coinCount = 0;
    public TMP_Text coinText;

    public void addCoin()
    {
        coinCount++;
        UpdateCoinText();
    }

    void Start()
    {
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        coinText.text = $"x {coinCount:00}";
    }
}
