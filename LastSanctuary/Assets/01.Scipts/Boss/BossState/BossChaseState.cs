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
        //애니메이션 실행
        StartAnimation(_boss.AnimationDB.WalkParameterHash);
        
        SoundClip[0] = _data.walkSound;
    }
    
    public override void Exit()
    {
        //애니메이션 종료
        StopAnimation(_boss.AnimationDB.WalkParameterHash);
    }
    
    public override void Update()
    {
        base.Update();
        
        //타겟이 사라지거나, 추적 사정거리에 안에 있으면
        if (_boss.Target == null || WithinChaseDistance())
        {
            //대기 상태로 이동
            _stateMachine.ChangeState(_stateMachine.IdleState);
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
}