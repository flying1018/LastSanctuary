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
        
        Vector2 bossPos = _boss.transform.position;
        Vector2 targetPos = _boss.Target.position;
        targetPos.y = bossPos.y;

        float distance = Vector2.Distance(bossPos, targetPos);
        
        if (distance <= _data.attackRange)// 일정 거리 안에 들어오면 Idle 상태로 전환
        {
            //Debug.Log("가까워짐 대기상태 진입");
            _rigidbody.velocity = Vector2.zero;
            _stateMachine.ChangeState(_stateMachine.IdleState);
            return;
        }
        
        Vector2 direction = (targetPos - bossPos).normalized;
        _rigidbody.velocity = direction * _data.moveSpeed;
    }
}
