using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public static CoinSpawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject SpawnCoin(Vector3 position, Coin.CoinType coinType)
    {
        string poolTag = GetPoolTag(coinType);
        return ObjectPool.Instance.SpawnFromPool(poolTag, position, Quaternion.identity);
    }

    private string GetPoolTag(Coin.CoinType coinType)
    {
        switch (coinType)
        {
            case Coin.CoinType.Bronze: return "BronzeCoin";
            case Coin.CoinType.Silver: return "SilverCoin";
            case Coin.CoinType.Gold: return "GoldCoin";
            case Coin.CoinType.Diamond: return "DiamondCoin";
            default: return "BronzeCoin";
        }
    }

    public GameObject SpawnRandomCoin(Vector3 position)
    {
        Coin.CoinType randomType = (Coin.CoinType)Random.Range(0, 4);
        return SpawnCoin(position, randomType);
    }
}