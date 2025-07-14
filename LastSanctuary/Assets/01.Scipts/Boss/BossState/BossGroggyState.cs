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
        
        //외곽선 변경
        _spriteRenderer.material = _data.materials[2];
    }

    public override void Exit()
    {
        _condition.IsGroggy = false;
        StopAnimation(_boss.AnimationDB.GroggyParameterHash);
        
        //외곽선 복구
        _spriteRenderer.material = _boss.Phase2 ? _data.materials[1] : _data.materials[0];
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
