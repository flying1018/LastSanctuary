using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    private float _hitStart;
    public override void Enter()
    {
        //피격 애니메이션
        _hitStart = Time.time;
    }

    public override void Exit()
    {
        
    }

    public override void PhysicsUpdate()
    {
        if (Time.time - _hitStart >= _data.HitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
