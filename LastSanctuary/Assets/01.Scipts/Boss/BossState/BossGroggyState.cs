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

    public override void Update()
    {
        if (Time.time - _groggyStart < _data.groggyDuration)
            return;
        if (_condition.IsPhaseShift())
        {
            _stateMachine.ChangeState(_stateMachine.PhaseShiftState);
        }
        else
        { 
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        
    }
}
