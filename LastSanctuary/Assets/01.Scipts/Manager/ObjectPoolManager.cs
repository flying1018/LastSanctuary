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
            objectQueue.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newQueue = new();
            newQueue.Enqueue(gameObject);
            poolDictionary.Add(id, newQueue);
        }
    }
}
