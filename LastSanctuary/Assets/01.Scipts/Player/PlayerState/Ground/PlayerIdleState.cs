using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (_input.IsDash)
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }

        if (_input.IsJump)
        {
            _stateMachine.ChangeState(_stateMachine.JumpState); 
        }
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(_input.MoveInput.x) > 0f)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        Move();
    }

}
