using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        _rigidbody.AddForce(Vector2.up * _playerSO.jumpForce, ForceMode2D.Impulse);

        _stateMachine.ChangeState(_stateMachine.AirState);
    }

    public override void Exit()
    {
        base.Enter();
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
