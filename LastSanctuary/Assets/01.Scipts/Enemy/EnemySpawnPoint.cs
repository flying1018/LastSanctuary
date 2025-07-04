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
        GameObject go =Instantiate(monster, transform.position, transform.rotation);
        _enemy = go.GetComponent<Enemy>();
        _enemy.Init(this.transform);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (enemy == _enemy)
            {
                _enemy.StateMachine.ChangeState(_enemy.StateMachine.ReturnState);
            }
        }
    }
}
