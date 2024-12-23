using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // * Reference to the coin manager that tracks collected coins
    private CoinManager coinManager;

    // * Find and store the coin manager reference
    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    // * Check for collision with coins and collect them

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
                UniqueID coinID = coin.GetComponent<UniqueID>();
                if (coinID != null)
                {
                    SaveLoadManager.Instance.RegisterCollectedCoin(coinID.uniqueID);
                }

                coinManager.addCoin(coin);
                Destroy(collision.gameObject);
            }
            else
            {
                Debug.LogError("Coin component not found on the collided object.");
            }
        }
    }
}