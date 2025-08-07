using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02IdleState : BossBaseState
{
    public Boss02IdleState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }
    
    public override void Enter()
    {
        //애니메이션 시작
        StartAnimation(_boss2.AnimationDB.IdleParameterHash);
        
        //이동 정지
        Move(Vector2.zero);
    }

    public override void Exit()
    {
        //애니메이션 종료
        StopAnimation(_boss2.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        _stateMachine2.AreaAttack.CheckCoolTime();
        
        _time += Time.deltaTime;
        if (_time > _data.attackIdleTime)
        {
            //시간 초기화
            _time = 0;

            SetMovePosition();
            _stateMachine2.ChangeState(_stateMachine2.TeleportState);
        }
    }

    protected void SetMovePosition()
    {
        if (_boss2.Phase2)
            _stateMachine2.MoveTarget = _boss02Event.GetRandomTopPosition();
        else
            _stateMachine2.MoveTarget = _boss02Event.GetRandomMirror();
    }
    
}
