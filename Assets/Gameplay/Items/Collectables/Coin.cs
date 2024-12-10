using UnityEngine;

public class Coin : MonoBehaviour
{
    public enum CoinType
    {
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    public CoinType coinType;
    public int value;

    private void Awake()
    {
        value = GetCoinValue(coinType);
    }

    private int GetCoinValue(CoinType type)
    {
        switch (type)
        {
            case CoinType.Bronze: return 1;
            case CoinType.Silver: return 2;
            case CoinType.Gold: return 3;
            case CoinType.Diamond: return 5;
            default: return 0;
        }
    }
}