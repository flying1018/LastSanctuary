using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 전체를 관리하는 게임 매니저
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public bool[] IsPlayerClear; // 테스트용 추후 삭제

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void Init() { }
}
