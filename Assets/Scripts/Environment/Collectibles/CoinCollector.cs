using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    // * Reference to the coin manager that tracks collected coins
    private CoinManager coinManager;
    public AudioSource coinCollectSound;

    // * Find and store the coin manager reference
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
                coinCollectSound.Play();
                coin.Collect();
            }
            else
            {
                Debug.LogError("Coin component not found on the collided object.");
            }
        }
    }
}