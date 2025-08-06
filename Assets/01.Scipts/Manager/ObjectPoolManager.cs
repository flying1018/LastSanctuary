using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolingIndex
{
    Monster = 1,
    Arrow = 2,
    Item = 3,
    BossProjectile = 4,
    SFX = 5,
    PlayerGroggy = 6,
    PlayerUlt = 7,
    UltParticle1 = 8,
    UltParticle2 = 9,
    Boss02Projectile1 = 10,
    Boss02Projectile2 = 11,
}

public static class ObjectPoolManager
{
    private static Dictionary<int, Queue<GameObject>> poolDictionary = new();


    //오브젝트 호출
    public static GameObject Get(GameObject prefab, int id)
    {
        //딕셔너리에 존재한다면 호출
        if (poolDictionary.TryGetValue(id, out Queue<GameObject> objectQueue))
        {
            while (objectQueue.Count > 0)
            {
                GameObject obj = objectQueue.Dequeue();
                if (obj != null)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
        }

        //딕셔너리에 없다면 생성
        GameObject newObj = Object.Instantiate(prefab);
        newObj.SetActive(true);
        return newObj;
    }

    //오브젝트 회수
    public static void Set(GameObject gameObject, int id)
    {
        gameObject.SetActive(false);

        //딕셔너리에 존재한다면 회수
        if (poolDictionary.TryGetValue(id, out Queue<GameObject> objectQueue))
        {
            objectQueue.Enqueue(gameObject);
        }
        //딕셔너리에 존재하지 않는다면 생성
        else
        {
            Queue<GameObject> newQueue = new();
            newQueue.Enqueue(gameObject);
            poolDictionary.Add(id, newQueue);
        }
    }
}
