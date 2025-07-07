using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<EnemySpawnPoint> SpawnPoints;

    public void RespawnEnemies()
    {
        foreach(var spawnPoint in SpawnPoints) spawnPoint.Respawn();
    }
}
