using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public enum EnemyType
    {
        BlueSlime,
        BrownSlime,
        GraySlime,
        GreenSlime,
        Mushroom
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject SpawnEnemy(EnemyType type, Vector3 position)
    {
        string enemyTag = GetEnemyTag(type);
        return ObjectPool.Instance.SpawnFromPool(enemyTag, position, Quaternion.identity);
    }

    private string GetEnemyTag(EnemyType type)
    {
        switch(type)
        {
            case EnemyType.BlueSlime: return "BlueSlime";
            case EnemyType.BrownSlime: return "BrownSlime";
            case EnemyType.GraySlime: return "GraySlime";
            case EnemyType.GreenSlime: return "GreenSlime";
            case EnemyType.Mushroom: return "Mushroom";
            default: return "BlueSlime";
        }
    }
}