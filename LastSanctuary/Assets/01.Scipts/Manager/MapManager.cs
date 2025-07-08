using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<EnemySpawnPoint> SpawnPoints;
    [SerializeField] private List<WarpObject> warpObjects;

    public WarpObject selectWarpObj;
    public WarpObject targetWarpObj;
    
    public void RespawnEnemies()
    {
        foreach (var spawnPoint in SpawnPoints) spawnPoint.Respawn();
    }

    public void WarpInteract()
    {
        targetWarpObj = warpObjects[UIManager.Instance.ShowWarpUI(selectWarpObj) - 1];
        //player.transform.position = targetWarpObj.WarpTransform.position;

    }
}
