using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject item;
    
    private GameObject _curItem;
    private Coroutine _respawnCoroutine;
    [SerializeField] private float respawnCoolTime = 10;
    
    
    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    //아이템 스폰
    public void Spawn()
    {
        _curItem =ObjectPoolManager.Get(item,(int)PoolingIndex.Item);
        _curItem.transform.position = transform.position;

        if (_curItem.TryGetComponent(out StatObject statObject))
        { 
            statObject.OnInteracte += () => _respawnCoroutine = StartCoroutine(RespawmCoroutine());
        }
    }
    

    //아이템 리스폰
    public void Respawn()
    {
        //리스폰시 동작중인 코루틴 정지
        if (_respawnCoroutine != null)
        {
            StopCoroutine(_respawnCoroutine);
            _respawnCoroutine = null;
        }

        if (_curItem == null)
        {
            Spawn();
            return;
        }
        StatObject statObject = _curItem.GetComponent<StatObject>();
        statObject.IsGet = false;
        statObject.SetActive();
    }

    private IEnumerator RespawmCoroutine()
    {
        yield return new WaitForSeconds(respawnCoolTime);
        Respawn();
    }
}
