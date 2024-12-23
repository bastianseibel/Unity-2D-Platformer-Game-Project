using UnityEngine;

[System.Serializable]
public class SaveData
{
   public int totalCoins;
   public bool[] unlockedLevels;
   public int lastUnlockedLevel;

   // * Constructor for initializing save data
   public SaveData()
   {
      totalCoins = 0;
      unlockedLevels = new bool[2];
      unlockedLevels[0] = true;
      lastUnlockedLevel = 0;
   }
}