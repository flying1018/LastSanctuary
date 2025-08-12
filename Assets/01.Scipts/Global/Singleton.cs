using UnityEngine;


/// <summary>
/// 싱글톤 추상 클래스
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}