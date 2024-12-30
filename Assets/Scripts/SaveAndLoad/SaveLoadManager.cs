using UnityEngine;
using System.IO;
using System.Threading.Tasks;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; private set; }
    private string savePath;
    private SaveData saveData;
    private bool isSaving;

    private const int LEVEL_COUNT = 2;
    private const string SAVE_FILENAME = "gameData.json";

    public int totalCoins => saveData.totalCoins;
    public bool[] unlockedLevels => saveData.unlockedLevels;
    public int lastUnlockedLevel => saveData.lastUnlockedLevel;
    public float[] levelBestTimes => saveData.levelBestTimes;

    private void Awake()
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

    private void InitializeAndLoad()
    {
        savePath = Path.Combine(Application.persistentDataPath, SAVE_FILENAME);
        LoadProgress();
    }

    private void LoadProgress()
    {
        try
        {
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                saveData = new SaveData(LEVEL_COUNT);
                JsonUtility.FromJsonOverwrite(jsonData, saveData);
            }
            else
            {
                InitializeNewSave();
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading save data: {e.Message}");
            InitializeNewSave();
        }
    }

    private void InitializeNewSave()
    {
        saveData = new SaveData(LEVEL_COUNT);
        SaveProgress();
    }

    private async void SaveProgress()
    {
        if (isSaving) return;

        isSaving = true;

        try
        {
            string jsonData = JsonUtility.ToJson(saveData, true);
            await Task.Run(() =>
            {
                File.WriteAllText(savePath, jsonData);
            });
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error saving game progress: {e.Message}");
        }
        finally
        {
            isSaving = false;
        }
    }

    public void AddCoins(int amount)
    {
        saveData.totalCoins += amount;
        SaveProgress();
    }

    public void SaveLevelTime(int levelIndex, float time)
    {
        if (levelIndex >= 0 && levelIndex < saveData.levelBestTimes.Length)
        {
            if (time < saveData.levelBestTimes[levelIndex])
            {
                saveData.levelBestTimes[levelIndex] = time;
                SaveProgress();
            }
        }
    }

    public float GetBestTime(int levelIndex)
    {
        if (levelIndex < saveData.levelBestTimes.Length)
        {
            return saveData.levelBestTimes[levelIndex];
        }
        return float.MaxValue;
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex < saveData.unlockedLevels.Length)
        {
            saveData.unlockedLevels[levelIndex] = true;
            if (levelIndex > saveData.lastUnlockedLevel)
            {
                saveData.lastUnlockedLevel = levelIndex;
            }
            SaveProgress();
        }
    }

    public bool IsLevelUnlocked(int levelIndex)
    {
        return levelIndex < saveData.unlockedLevels.Length && saveData.unlockedLevels[levelIndex];
    }
}