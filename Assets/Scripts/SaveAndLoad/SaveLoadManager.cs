using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string savePath;

    public int totalCoins { get; private set; }
    public bool[] unlockedLevels = new bool[2];
    public int lastUnlockedLevel = 0;


    // * Singleton pattern
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "gameProgress.dat");
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // * Load game progress from file
    private void LoadProgress()
    {
        if (!File.Exists(savePath))
        {
            InitializeNewSave();
            return;
        }

        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                SaveData saveData = formatter.Deserialize(stream) as SaveData;
                totalCoins = saveData.totalCoins;
                unlockedLevels = saveData.unlockedLevels;
                lastUnlockedLevel = saveData.lastUnlockedLevel;
            }
            Debug.Log($"Game progress loaded - Coins: {totalCoins}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading game progress: {e.Message}");
            InitializeNewSave();
        }
    }

    // * Initialize new save
    private void InitializeNewSave()
    {
        totalCoins = 0;
        unlockedLevels = new bool[2];
        unlockedLevels[0] = true;
        lastUnlockedLevel = 0;
        SaveProgress();
    }

    // * Save game progress to file
    public void SaveProgress()
    {
        try
        {
            SaveData saveData = new SaveData
            {
                totalCoins = totalCoins,
                unlockedLevels = unlockedLevels,
                lastUnlockedLevel = lastUnlockedLevel
            };

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                formatter.Serialize(stream, saveData);
            }
            Debug.Log($"Game progress saved - Coins: {totalCoins}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving game progress: {e.Message}");
        }
    }

    // * Add coins to total
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveProgress();
    }

    // * Unlock a level
    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex < unlockedLevels.Length)
        {
            unlockedLevels[levelIndex] = true;
            if (levelIndex > lastUnlockedLevel)
            {
                lastUnlockedLevel = levelIndex;
            }
            SaveProgress();
        }
    }

    // * Check if a level is unlocked
    public bool IsLevelUnlocked(int levelIndex)
    {
        return levelIndex < unlockedLevels.Length && unlockedLevels[levelIndex];
    }
}