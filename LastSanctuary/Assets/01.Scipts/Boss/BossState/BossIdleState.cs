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
        base.Update();
        
        if (_boss.Target)
        {
            Debug.Log("플레이어 인식");
             _stateMachine.ChangeState(_stateMachine.ChaseState); //추적상태로 전환
        }
    }
}
