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
        StartAnimation(_boss.AnimationDB.IdleParameterHash);
        _rigidbody.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        StopAnimation(_boss.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        
        if (!WithinChaseDistance()) //추적거리 외에 있을때
        {
            Debug.Log("플레이어 인식");
             _stateMachine.ChangeState(_stateMachine.ChaseState); //추적상태로 전환
        }

        if (WithAttackDistance()) //공격거리 내에 있을때
        {
            Debug.Log("공격시작");
            _stateMachine.ChangeState(_stateMachine.Attack1);
        }
    }
}
