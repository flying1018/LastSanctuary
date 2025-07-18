using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerBaseState
{
    private float _guardStart;

    public PlayerGuardState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.GuardParameterHash);
        
        //방어에 필요한 데이터 설정
        _guardStart = Time.time;
        _condition.IsPerfectGuard = true;
        _condition.IsGuard = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.GuardParameterHash);
        
        //방어 종료
        _condition.IsGuard = false;
        _condition.IsPerfectGuard = false;
    }

    public override void HandleInput()
    {
        //가드 해제 시
        if (!_input.IsGuarding)
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        //대쉬 키 입력 시 스테미나가 충분하면
        if (_input.IsDash && _stateMachine.DashState.UseCanDash())
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState); 
        }
    }
    
    public override void PhysicsUpdate()
    {
        PerfectGuard();
    }
    
    //퍼펙트 가드
    public void PerfectGuard()
    {
        if (Time.time - _guardStart >= _data.perfectGuardWindow)
        {
            _condition.IsPerfectGuard = false;
        }
    }
    
    public override void Update()
    {
        
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.guardSound);
    }
    
    public override void PlaySFX2()
    {
        SoundManager.Instance.PlaySFX(_data.perfectGuardSound);
    }
}
