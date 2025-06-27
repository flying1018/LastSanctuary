using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGuardState : PlayerGroundState
{
    private float _perfectGuardWindow = 0.2f;
    private float _guardStart;
    
    public bool _isPrefectGuard;
    public PlayerGuardState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _rigidbody.velocity = Vector2.zero;
        _guardStart = Time.time;
        _isPrefectGuard = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (!_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        Guard();
    }
    
    public void Guard()
    {
        //PerfectGuard
        if (_input.IsGuarding)
        {
            Debug.Log("가드 성공");
        }
    }
}
