using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    //필드
    private Enemy _enemy;
    private Coroutine _cancelChase;
    [SerializeField] private float patrolDistance = 10;
    public bool isSpawn {get; set;}

    //직렬화
    [SerializeField] private GameObject monster;

    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    //몬스터 스폰
    public void Spawn()
    {
        GameObject go =ObjectPoolManager.Get(monster,(int)ObjectPoolManager.PoolingIndex.Monster);
        go.transform.position = transform.position;
        _enemy = go.GetComponent<Enemy>();
        _enemy.Init(this.transform, patrolDistance);
        isSpawn = true;
    }

    public void Respawn()
    {
        if(!isSpawn)
            Spawn();
        else
        {
            //살아 있는 몬스터 위치 초기화
            _enemy.transform.position = transform.position;
            _enemy.StateMachine.ChangeState(_enemy.StateMachine.IdleState);
        }
    
    }
}
