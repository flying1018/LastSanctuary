using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private float _time;
    
    public BossIdleState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_boss.AnimationDB.IdleParameterHash);
        _time = 0;
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
             _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
        
        _time += Time.deltaTime;
        if (_time >= _data.attackIdleTime)
        {
            if (_stateMachine.Attacks.Count <= 0) return;
            _stateMachine.ChangeState(_stateMachine.Attacks.Dequeue());
        }
    }
}
