using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject item;
    
    private GameObject curItem;
    private Coroutine coroutine;    //어떤 코루틴인지 명시 필요
    [SerializeField] private float _respawnCoolTime = 10;
    
    
    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    //아이템 스폰
    public void Spawn()
    {
        curItem =ObjectPoolManager.Get(item,(int)ObjectPoolManager.PoolingIndex.Item);
        curItem.transform.position = transform.position;

        if (curItem.TryGetComponent(out StatObject statObject))
        { 
            statObject.OnInteracte += () => coroutine = StartCoroutine(RespawmCoroutine());
        }
    }
    

    //아이템 리스폰
    public void Respawn()
    {
        //리스폰시 동작중인 코루틴 정지
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        if (curItem == null)
        {
            Spawn();
            return;
        }
        StatObject statObject = curItem.GetComponent<StatObject>();
        statObject.IsGet = false;
        statObject.SetActive();
    }

    private IEnumerator RespawmCoroutine()
    {
        yield return new WaitForSeconds(_respawnCoolTime);
        Respawn();
    }
}
