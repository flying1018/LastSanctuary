using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerGroundState
{
    private float _perfectGuardWindow = 0.2f;
    private float _guardStart;
    
    public bool _isPrefectGuard;
    public bool _isGuard;
    public PlayerGuardState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
       Debug.Log("Enter");
        _rigidbody.velocity = Vector2.zero;
        _guardStart = Time.time;
        _isPrefectGuard = true;
        _isGuard = true;
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
            _isGuard = false;
            _isPrefectGuard = false;
            _stateMachine.ChangeState(_stateMachine.IdleState);
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
            _isPrefectGuard = false;
        }
    }
}
