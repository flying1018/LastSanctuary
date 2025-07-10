using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseShiftState : BossBaseState
{
    private float _animeTime;
    private float _time;
    public BossPhaseShiftState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
        _animeTime = _data.PhaseShiftTime;
    }

    public override void Enter()
    {
        _boss.Animator.SetTrigger(_boss.AnimationDB.PhaseShiftParameterHash);

        _boss.Phase2 = true;
    }

    public override void Update()
    {
        base.Update();
        _time += Time.deltaTime;
        if(_time > _animeTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
}
