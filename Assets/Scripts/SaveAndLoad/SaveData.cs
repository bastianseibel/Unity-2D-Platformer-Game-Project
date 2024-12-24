using UnityEngine;

[System.Serializable]
public class SaveData
{
   public int totalCoins;
   public bool[] unlockedLevels;
   public int lastUnlockedLevel;
   public float[] levelBestTimes;

   // * Constructor for initializing save data
   public SaveData()
   {
      totalCoins = 0;
      unlockedLevels = new bool[2];
      unlockedLevels[0] = true;
      lastUnlockedLevel = 0;
      levelBestTimes = new float[2];
      for (int i = 0; i < levelBestTimes.Length; i++)
      {
         levelBestTimes[i] = float.MaxValue;
      }
   }
}