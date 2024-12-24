using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string savePath;

    public int totalCoins { get; private set; }
    public bool[] unlockedLevels;
    public int lastUnlockedLevel = 0;
    public float[] levelBestTimes;

    private const int LEVEL_COUNT = 2;


    // * Singleton Pattern 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAndLoad();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // * Initialize and Load
    private void InitializeAndLoad()
    {
        savePath = Path.Combine(Application.persistentDataPath, "gameProgress.dat");
        InitializeArrays();
        LoadProgress();
    }

    // * Initialize Arrays
    private void InitializeArrays()
    {
        unlockedLevels = new bool[LEVEL_COUNT];
        unlockedLevels[0] = true;
        levelBestTimes = new float[LEVEL_COUNT];
        for (int i = 0; i < LEVEL_COUNT; i++)
        {
            levelBestTimes[i] = float.MaxValue;
        }
    }
    // * Load Progress
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
                levelBestTimes = saveData.levelBestTimes;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading game progress: {e.Message}");
            InitializeNewSave();
        }
    }

    // * Initialize New Save
    private void InitializeNewSave()
    {
        totalCoins = 0;
        InitializeArrays();
        SaveProgress();
    }

    // * Save Level Time
    public void SaveLevelTime(int levelIndex, float time)
    {
        if (levelIndex >= 0 && levelIndex < levelBestTimes.Length && time < levelBestTimes[levelIndex])
        {
            levelBestTimes[levelIndex] = time;
            SaveProgress();
        }
    }

    // * Get Best Time
    public float GetBestTime(int levelIndex)
    {
        if (levelIndex < levelBestTimes.Length)
        {
            return levelBestTimes[levelIndex];
        }
        return float.MaxValue;
    }

    // * Save Progress
    public void SaveProgress()
    {
        try
        {
            SaveData saveData = new SaveData
            {
                totalCoins = totalCoins,
                unlockedLevels = unlockedLevels,
                lastUnlockedLevel = lastUnlockedLevel,
                levelBestTimes = levelBestTimes
            };

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            {
                formatter.Serialize(stream, saveData);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving game progress: {e.Message}");
        }
    }

    // * Add Coins
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        SaveProgress();
    }

    // * Unlock Level
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

    // * Is Level Unlocked
    public bool IsLevelUnlocked(int levelIndex)
    {
        return levelIndex < unlockedLevels.Length && unlockedLevels[levelIndex];
    }
}