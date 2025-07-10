using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    public BossChaseState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }
    
    public override void Enter()
    {
        StartAnimation(_boss.AnimationDB.WalkParameterHash);
    }
    
    public override void Exit()
    {
        StopAnimation(_boss.AnimationDB.WalkParameterHash);
    }
    
    public override void Update()
    {
        if (_boss.Target == null)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return; 
        }
        
        if (WithinChaseDistance()) 
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Vector2 direction = DirectionToTarget();
        Move(direction);
        Rotate(direction);
    }
    
    public Vector2 DirectionToTarget()
    {
        if(_boss.Target == null) return Vector2.zero; //방어코드
       return _boss.Target.position - _boss.transform.position; //플레이어 방향
    }
}