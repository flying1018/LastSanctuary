using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<EnemySpawnPoint> EnemySpawnPoints;
    [SerializeField] private List<ItemSpawnPoint> ItemSpawnPoints;
    [SerializeField] private List<WarpObject> warpObjects;

    public WarpObject selectWarpObj;
    public WarpObject targetWarpObj;
    
    public void RespawnEnemies()
    {
        foreach (var spawnPoint in EnemySpawnPoints) spawnPoint.Respawn();
    }

    public void RespawnItems()
    {
        foreach (var spawnPoint in ItemSpawnPoints) spawnPoint.Respawn(); 
    }

    public void WarpInteract()
    {
        targetWarpObj = warpObjects[UIManager.Instance.ShowWarpUI(selectWarpObj) - 1];
        //player.transform.position = targetWarpObj.WarpTransform.position;

    }
}
