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
        Debug.Log("추적 시작");
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

        Vector2 direction = DirectionToTarget();
        Rotate(direction);
        Move(direction);
    }
    
    public Vector2 DirectionToTarget()
    {
        if(_boss.Target == null) return Vector2.zero;
       return _boss.Target.position - _boss.transform.position; //플레이어 방향
    }
    
    public void Rotate(Vector2 direction)
    {
        _rigidbody.velocity = direction * _data.moveSpeed;
    }

    public void Move(Vector2 direction)
    {
        _rigidbody.velocity = direction * _data.moveSpeed;
    }
}
