using UnityEngine;

[System.Serializable]
public class PlayerSaveData
{
   public float positionX;
   public float positionY;
   public float positionZ;
   public int currentHealth;
   public int coinCount;

   public float checkpointX;
   public float checkpointY;
   public float checkpointZ;
   public bool hasCheckpoint;

   public string[] collectedCoinIDs;
   public string[] defeatedEnemyIDs;  
}