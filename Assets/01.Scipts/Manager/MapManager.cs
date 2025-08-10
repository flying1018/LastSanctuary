using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    Tutorial, 
    First, 
    Second, 
    Third, 
    Fourth,
}


/// <summary>
/// 맵의 배치된 오브젝트를 관리하는 맵 매니저
/// </summary>
public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<EnemySpawnPoint> EnemySpawnPoints;
    [SerializeField] private List<ItemSpawnPoint> ItemSpawnPoints;
    [SerializeField] private List<WarpObject> warpObjects;
    [SerializeField] private MapType map;

    private List<EnemySpawnPoint> deadElites = new List<EnemySpawnPoint>();

    public WarpObject selectWarpObj;
    public WarpObject targetWarpObj;
    
    public static bool IsBossAlive { get; private set; }


    private void Awake()
    {
        //스폰 포인트 가져오기
        EnemySpawnPoints = new List<EnemySpawnPoint>(FindObjectsOfType<EnemySpawnPoint>());
        ItemSpawnPoints = new List<ItemSpawnPoint>(FindObjectsOfType<ItemSpawnPoint>());
        IsBossAlive = true;

    }

  

    public static void SetBossDead()
    {
        IsBossAlive = false;
    }

    public void SetEliteDead(EnemySpawnPoint spawnPoint)
    {
        if (!deadElites.Contains(spawnPoint))
            deadElites.Add(spawnPoint);
    }

    //몬스터 리스폰
    public void RespawnMap()
    {
        RespawnEnemys();
        RespawnItems();
    }

    public void RespawnEnemys()
    {
        foreach (var spawnPoint in EnemySpawnPoints)
        {
            if (spawnPoint.Enemytype == EnemyType.Elite && deadElites.Contains(spawnPoint))
                continue;
            
            spawnPoint.Respawn();
        }
    }

    public void RespawnItems()
    {
        foreach (var spawnPoint in ItemSpawnPoints) spawnPoint.Respawn(); 
    }
}
