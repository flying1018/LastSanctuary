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
    }

    public override void Exit()
    {
        StopAnimation(_boss.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        if (_boss.Target)
        {
             _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
    }
}
