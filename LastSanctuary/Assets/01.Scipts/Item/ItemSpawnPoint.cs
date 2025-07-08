using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject item;
    
    private GameObject curItem;
    private Coroutine coroutine;
    [SerializeField] private float _respawnCoolTime = 10;
    
    
    private void Start()
    {
        //Test Code
        Spawn();
    }
    
    public void Spawn()
    {
        curItem =ObjectPoolManager.Get(item,(int)ObjectPoolManager.PoolingIndex.Item);
        curItem.transform.position = transform.position;
        
        StatObject statObject = curItem.GetComponent<StatObject>();
        statObject.OnInteracte += () => coroutine = StartCoroutine(RespawmCoroutine());
    }
    

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
