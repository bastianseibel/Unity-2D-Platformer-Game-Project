using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string savePath;
    public List<string> collectedCoins = new List<string>();
    public List<string> defeatedEnemies = new List<string>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
            Debug.Log("Spiel gespeichert!");
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
            Debug.Log("Spiel geladen!");
        }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "gamesave.dat");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterCollectedCoin(string coinID)
    {
        if (!collectedCoins.Contains(coinID))
        {
            collectedCoins.Add(coinID);
        }
    }

    public void RegisterDefeatedEnemy(string enemyID)
    {
        if (!defeatedEnemies.Contains(enemyID))
        {
            defeatedEnemies.Add(enemyID);
        }
    }

    public void SaveGame()
    {
        try
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            HeroHealth heroHealth = player.GetComponent<HeroHealth>();
            CoinManager coinManager = FindObjectOfType<CoinManager>();

            PlayerSaveData saveData = new PlayerSaveData
            {
                positionX = player.transform.position.x,
                positionY = player.transform.position.y,
                positionZ = player.transform.position.z,
                currentHealth = heroHealth.currentHealth,
                coinCount = coinManager.coinCount,

                checkpointX = heroHealth.spawnPoint.x,
                checkpointY = heroHealth.spawnPoint.y,
                checkpointZ = heroHealth.spawnPoint.z,
                hasCheckpoint = (heroHealth.spawnPoint != player.transform.position),

                collectedCoinIDs = collectedCoins.ToArray(),
                defeatedEnemyIDs = defeatedEnemies.ToArray()
            };

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                formatter.Serialize(stream, saveData);
            }

            Debug.Log("Spiel gespeichert!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Fehler beim Speichern: {e.Message}");
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(savePath)) return;

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            PlayerSaveData saveData;
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                saveData = formatter.Deserialize(stream) as PlayerSaveData;
            }

            collectedCoins = new List<string>(saveData.collectedCoinIDs);
            defeatedEnemies = new List<string>(saveData.defeatedEnemyIDs);

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            HeroHealth heroHealth = player.GetComponent<HeroHealth>();
            CoinManager coinManager = FindObjectOfType<CoinManager>();

            if (saveData.hasCheckpoint)
            {
                Vector3 checkpointPos = new Vector3(saveData.checkpointX, saveData.checkpointY, saveData.checkpointZ);
                heroHealth.SetSpawnPoint(checkpointPos);
                player.transform.position = checkpointPos;
            }
            else
            {
                player.transform.position = new Vector3(saveData.positionX, saveData.positionY, saveData.positionZ);
            }

            heroHealth.currentHealth = saveData.currentHealth;
            coinManager.coinCount = saveData.coinCount;
            coinManager.UpdateCoinText();

            foreach (Coin coin in FindObjectsOfType<Coin>())
            {
                UniqueID coinID = coin.GetComponent<UniqueID>();
                if (coinID != null && collectedCoins.Contains(coinID.uniqueID))
                {
                    Destroy(coin.gameObject);
                }
            }

            foreach (EnemieManager enemy in FindObjectsOfType<EnemieManager>())
            {
                UniqueID enemyID = enemy.GetComponent<UniqueID>();
                if (enemyID != null && defeatedEnemies.Contains(enemyID.uniqueID))
                {
                    Destroy(enemy.gameObject);
                }
            }

            Debug.Log("Spiel geladen!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Fehler beim Laden: {e.Message}");
        }
    }
}