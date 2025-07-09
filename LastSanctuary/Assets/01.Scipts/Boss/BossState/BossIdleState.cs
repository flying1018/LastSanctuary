using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    public BossIdleState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("대기상태 진입");
        StartAnimation(_boss.AnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        StopAnimation(_boss.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        if (!_boss.HasDetectedTarget)
        {
            float distance = Vector2.Distance(_boss.transform.position, _boss.Target.position);    // 감지 범위 안에 플레이어가 들어왔는지 확인
            if (distance <= 20f) // 감지 범위 (유닛범위)
            {
                _boss.DetectPlayer(_boss.Target);
                Debug.Log("플레이어 감지됨");
            }
        }

        if (_boss.HasDetectedTarget)
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
    }
}
