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

        _time = 0;
        _boss.Phase2 = true;
        
        Debug.Log("PahseShiftEnter");
    }

    public override void Exit()
    {
        Debug.Log("PahseShiftExit");
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if(_time > _animeTime)
        {
            Debug.Log("PahseShiftEnd");
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }
}
