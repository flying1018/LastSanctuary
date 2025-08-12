using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Elite,
}

public class EnemySpawnPoint : MonoBehaviour
{
    //필드
    private Enemy _enemy;
    private Coroutine _cancelChase;
    public bool isSpawn {get; set;}

    //직렬화
    [SerializeField] private GameObject monster;
    [SerializeField] private float patrolDistance = 10;
    [SerializeField] private EnemyType enemyType;
    public EnemyType Enemytype => enemyType;
    public float PatrolDistance => patrolDistance;
    
    //몬스터 스폰
    public void Spawn()
    {
        GameObject go =ObjectPoolManager.Get(monster,(int)PoolingIndex.Monster);
        go.transform.position = transform.position;
        go.transform.parent = transform;
        _enemy = go.GetComponent<Enemy>();
        _enemy.Init(this);
        isSpawn = true;
    }

    public void Respawn()
    {
        if(!isSpawn)
            Spawn();
        else
        {
            //살아 있는 몬스터 위치 초기화
            _enemy.ResetPosition();
        }
    
    }
}
