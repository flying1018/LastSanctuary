using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField] private List<EnemySpawnPoint> SpawnPoints;
    [SerializeField] private List<WarpObject> warpObjects;

    public WarpObject selectWarpObj;
    public WarpObject targetWarpObj;

    private static MapManager _instance;
    public static MapManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MapManager>();
                if (_instance == null)
                {
                    DebugHelper.LogError("MapManager 없음");
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

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
