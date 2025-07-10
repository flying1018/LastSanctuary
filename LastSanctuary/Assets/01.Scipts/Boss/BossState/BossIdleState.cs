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
        Debug.Log("대기상태 진입");
        StartAnimation(_boss.AnimationDB.IdleParameterHash);
        _rigidbody.velocity = Vector2.zero;

        _time = 0;
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
             _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
        
        _time += Time.deltaTime;
        if (_time >= _data.attackIdleTime && WithAttackDistance())
        {
            if (_stateMachine.Attacks.Count <= 0) return;
            _stateMachine.ChangeState(_stateMachine.Attacks.Dequeue());
        }
    }
}
