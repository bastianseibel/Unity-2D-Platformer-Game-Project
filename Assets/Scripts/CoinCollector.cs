using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    private CoinManager coinManager;

    void Start()
    {
        coinManager = FindObjectOfType<CoinManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Coin coin = collision.GetComponent<Coin>();
            if (coin != null)
            {
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