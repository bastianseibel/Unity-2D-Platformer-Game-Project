using UnityEngine;

public class Coin : MonoBehaviour, IPooledObject
{
    // * Different coin types and their values
    public enum CoinType
    {
        Bronze,
        Silver,
        Gold,
        Diamond
    }

    // * Coin properties
    public CoinType coinType;
    public int value;

    // * Set coin value based on type when created
    private void Awake()
    {
        value = GetCoinValue(coinType);
    }

    public void OnObjectSpawn()
    {
        // Reset beim Spawnen aus dem Pool
        value = GetCoinValue(coinType);
        gameObject.SetActive(true);
    }

    // * Get value based on coin type
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

    public void Collect()
    {
        gameObject.SetActive(false);
    }
}