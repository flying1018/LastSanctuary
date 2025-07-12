using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    public enum PoolingIndex
    {
        Monster = 1,
        Arrow = 2,
        Item = 3,
    }
    private static Dictionary<int, Queue<GameObject>> poolDictionary = new();

    public static GameObject Get(GameObject prefab, int id)
    {
        if (poolDictionary.TryGetValue(id, out Queue<GameObject> objectQueue))
        {
            while (objectQueue.Count > 0)
            { 
                GameObject obj = objectQueue.Dequeue();
                if (obj != null)
                {
                    DebugHelper.Log("기존 풀에서 재활용");
                    obj.SetActive(true);
                    return obj;
                }
            }
        }
        GameObject newObj = Object.Instantiate(prefab);
        newObj.SetActive(true);
        return newObj;
    }

    public static void Set(int id, GameObject _prefab, GameObject gameObject)
    {
        gameObject.SetActive(false);

        if (poolDictionary.TryGetValue(id, out Queue<GameObject> objectQueue))
        {
            DebugHelper.Log("기존 프리팹 회수");
            objectQueue.Enqueue(gameObject);
        }
        else
        {
            DebugHelper.Log("새로운 프리팹 회수");
            Queue<GameObject> newQueue = new();
            newQueue.Enqueue(gameObject);
            poolDictionary.Add(id, newQueue);
        }
    }
}
