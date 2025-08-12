
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 전체를 관리하는 게임 매니저
/// </summary>
public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
    protected void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void Init() { }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var player = FindObjectOfType<Player>();
        if (scene.buildIndex == 0)
        {
            Destroy(player.gameObject);
            Destroy(UIManager.Instance.gameObject);
        }

    }
}
