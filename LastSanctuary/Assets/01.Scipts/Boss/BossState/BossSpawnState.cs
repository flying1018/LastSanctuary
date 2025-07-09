using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : BossBaseState
{
    private float _elapsed;

    public BossSpawnState(BossStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        Debug.Log("스폰상태 진입");
        _elapsed = 0f;

        StartAnimation(_boss.AnimationDB.SpawnParameterHash);
        _rigidbody.velocity = Vector2.zero; // 움직임 정지
    }

    public override void Exit()
    {
        StopAnimation(_boss.AnimationDB.SpawnParameterHash);
        Debug.Log("스폰 상태 종료");
    }

    public override void Update()
    {
        _elapsed += Time.deltaTime; 
        if (_elapsed >= _data.spawnAnimeTime) //스폰 애니메이션 지속시간이 끝나면
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}