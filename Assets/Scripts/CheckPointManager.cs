using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public Vector2 playerPosition;
    public List<int> defeatedEnemies;
    public List<int> collectedItems;  
}

public class CheckPointManager : MonoBehaviour
{
     public static CheckPointManager Instance;
    private CheckpointData currentCheckpointData;

    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }

        currentCheckpointData = new CheckpointData
        {
            defeatedEnemies = new List<int>(),
            collectedItems = new List<int>()
        };
    }

    public void SaveCheckpoint(Vector2 playerPosition)
    {
        currentCheckpointData.playerPosition = playerPosition;
    }

    public void RecordDefeatedEnemy(int enemyId)
    {
        if (!currentCheckpointData.defeatedEnemies.Contains(enemyId))
        {
            currentCheckpointData.defeatedEnemies.Add(enemyId);
        }
    }

    public void RecordCollectedItem(int itemId)
    {
        if (!currentCheckpointData.collectedItems.Contains(itemId))
        {
            currentCheckpointData.collectedItems.Add(itemId);
        }
    }

    public CheckpointData GetCheckpointData()
    {
        return currentCheckpointData;
    }

}
