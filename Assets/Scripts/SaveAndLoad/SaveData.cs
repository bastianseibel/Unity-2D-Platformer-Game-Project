using UnityEngine;

[System.Serializable]
public class SaveData
{
   public int totalCoins;
   public bool[] unlockedLevels;
   public int lastUnlockedLevel;
   public float[] levelBestTimes;

   public SaveData(int levelCount = 2)
   {
      totalCoins = 0;
      unlockedLevels = new bool[levelCount];
      unlockedLevels[0] = true;
      lastUnlockedLevel = 0;
      levelBestTimes = new float[levelCount];

      for (int i = 0; i < levelBestTimes.Length; i++)
      {
         levelBestTimes[i] = float.MaxValue;
      }
   }
}