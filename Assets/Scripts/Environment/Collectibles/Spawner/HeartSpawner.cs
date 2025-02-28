using UnityEngine;

public class HeartSpawner : MonoBehaviour
{
    private const string HEART_POOL_TAG = "Heart";

    public void SpawnHeart(Vector3 position)
    {
        ObjectPool.Instance.SpawnFromPool(HEART_POOL_TAG, position, Quaternion.identity);
    }
} 