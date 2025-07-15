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
        //애니메이션 시작
        StartAnimation(_boss.AnimationDB.IdleParameterHash);
        
        //이동 정지
        Move(Vector2.zero);
        
        //시간 초기화
        _time = 0;
    }

    public override void Exit()
    {
        //애니메이션 종료
        StopAnimation(_boss.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        base.Update();
        
        //추적거리 외에 있을때
        if (!WithinChaseDistance()) 
        {    //추적
             _stateMachine.ChangeState(_stateMachine.ChaseState);
        }
        
        //공격 대기 시간과 공격 사정거리 안에 있다면
        _time += Time.deltaTime;
        if (_time >= _data.attackIdleTime && WithAttackDistance())
        {
            //쿨타임이 다찬 공격이 없다면 대기
            if (_stateMachine.Attacks.Count <= 0) return;
            
            //공격
            _stateMachine.ChangeState(_stateMachine.Attacks.Dequeue());
        }
    }
}
