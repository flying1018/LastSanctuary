using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    private float _hitStart;
    
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }
    
    
    public override void Enter()
    {
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.HitParameterHash);
        
        _hitStart = Time.time;
    }

    public override void Exit()
    {
        
    }

    //여긴 시간 체크가 물리 업데이트에서 되어 있음.
    public override void PhysicsUpdate()
    {
        if (Time.time - _hitStart >= _data.hitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.BattleState);
        }
    }
    
}
