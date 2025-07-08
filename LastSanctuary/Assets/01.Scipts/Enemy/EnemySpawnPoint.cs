using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    //필드
    private Enemy _enemy;
    private Coroutine _cancelChase;
    
    //직렬화
    [SerializeField] private GameObject monster;

    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    public void Spawn()
    {
        GameObject go =ObjectPoolManager.Get(monster,(int)ObjectPoolManager.PoolingIndex.Monster);
        go.transform.position = transform.position;
        _enemy = go.GetComponent<Enemy>();
        _enemy.Init(this.transform);
        _enemy.SetCollisionEnabled(true);
    }

    public void Respawn()
    {
        if (_enemy == null)
        {
            Spawn();
        }
        else
        {
            _enemy.transform.position = transform.position;
            _enemy.gameObject.SetActive(true);
            _enemy.Init(this.transform);
            _enemy.SetCollisionEnabled(true);
        }
    }
}
