using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerGroundState
{
    private float _perfectGuardWindow = 2f;
    private float _guardStart;

    public PlayerGuardState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
       Debug.Log("Enter");
        _rigidbody.velocity = Vector2.zero;
        _guardStart = Time.time;
        _condition.IsPerfectGuard = true;
        _condition.IsGuard = true;
        //가드 애니메이션 실행
    }

    public override void Exit()
    {
        base.Exit();
       Debug.Log("Exit");
        //가드 애니메이션 해제
    }

    public override void HandleInput()
    {
        if (!_input.IsGuarding)
        {
            _condition.IsGuard = false;
            _condition.IsPerfectGuard = false;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        if (_input.IsDash)
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        PerfectGuard();
    }
    
    public void PerfectGuard()
    {
        if (Time.time - _guardStart > _perfectGuardWindow)
        {
            _condition.IsPerfectGuard = false;
        }
    }
}
