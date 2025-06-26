using System.Collections;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public bool isDash;
    public bool isJump;
    private static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharcterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }
    }

    public Player _player;

    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

    }

    public void StartDashCool(float duration = 0.7f)
    {
        if (isDash)
            StartCoroutine(DashCool_Coroutine(duration));
    }

    private IEnumerator DashCool_Coroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDash = false;
        DebugHelper.Log("대시쿨 종료");
    }
}
