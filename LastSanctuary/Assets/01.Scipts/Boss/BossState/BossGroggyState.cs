using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroggyState : BossBaseState
{
    public BossGroggyState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    private float _groggyStart;
    public override void Enter()
    {
        _groggyStart = Time.time;
        _condition.IsGroggy = true;
        StartAnimation(_boss.AnimationDB.GroggyParameterHash);
    }

    public override void Exit()
    {
        _condition.IsGroggy = false;
        StopAnimation(_boss.AnimationDB.GroggyParameterHash);

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        if (Time.time - _groggyStart < _data.groggyDuration)
            return;
            
        _condition.ChangePhase2State();
        _stateMachine.ChangeState(_stateMachine.IdleState);
        
    }
}
