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
    

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if (_cancelChase != null)
            {
                StopCoroutine(_cancelChase);
                _cancelChase = null;
            }
            
            _enemy.Target = other.gameObject.transform;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            if (_cancelChase != null)
            {
                StopCoroutine(_cancelChase);
                _cancelChase = null;
            }

            _cancelChase = StartCoroutine(CancelChase_Coroutine());
        }
    }
    
    //인식 범위 밖으로 일정 시간 탈출 후 추적 취소
    IEnumerator CancelChase_Coroutine()
    {
        yield return new WaitForSeconds(_enemy.Data.cancelChaseTime);
        _enemy.Target = null;
        _cancelChase = null;
    }
}
