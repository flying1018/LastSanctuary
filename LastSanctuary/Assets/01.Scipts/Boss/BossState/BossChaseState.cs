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
        Move(direction);
        Rotate(direction);

        if (WithinChaseDistance()) 
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
            //아이들 스테이트로 들어가서 3초 대기 
        }
    }
    
    public Vector2 DirectionToTarget()
    {
        if(_boss.Target == null) return Vector2.zero; //방어코드
       return _boss.Target.position - _boss.transform.position; //플레이어 방향
    }
    
    
    public void Move(Vector2 direction)
    {
         float xDirection = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0; //보스 좌우 움직임
         Vector2 moveVelocity = new Vector2(xDirection * _data.moveSpeed, _rigidbody.velocity.y);
         _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        _spriteRenderer.flipX = direction.x < 0; //보스의 방향
    }

    protected bool WithinChaseDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange/ 2;
    }
}