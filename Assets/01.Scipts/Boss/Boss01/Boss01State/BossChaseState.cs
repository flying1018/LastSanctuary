using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BossBaseState
{
    public BossChaseState(BossStateMachine bossStateMachine1) : base(bossStateMachine1)
    {
    }
    
    public override void Enter()
    {
        //애니메이션 실행
        StartAnimation(_boss.AnimationDB.WalkParameterHash);
    }
    
    public override void Exit()
    {
        //애니메이션 종료
        StopAnimation(_boss.AnimationDB.WalkParameterHash);
    }
    
    public override void Update()
    {
        //공격 쿨타임 체크
        if (_stateMachine1.Attack1.CheckCoolTime())
        {
            _stateMachine1.Attacks.Enqueue(_stateMachine1.Attack1);
        }
        
        if (_stateMachine1.Attack2.CheckCoolTime())
        {           
            _stateMachine1.Attacks.Enqueue(_stateMachine1.Attack2);
        }
        
        //3번째 공격 페이즈 2에서만 사용
        if (_boss.Phase2 && _stateMachine1.Attack3.CheckCoolTime())
        {           
            _stateMachine1.Attacks.Enqueue(_stateMachine1.Attack3);
        }
        
        //타겟이 사라지거나, 추적 사정거리에 안에 있으면
        if (_boss.Target == null || WithinChaseDistance())
        {
            //대기 상태로 이동
            _stateMachine1.ChangeState(_stateMachine1.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        //추적
        Chase();
    }

    public void Chase()
    {
        Vector2 direction = DirectionToTarget();    //타겟의 방향
        Move(direction);    //이동
        Rotate(direction);  //회전
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.walkSound);
    }
}