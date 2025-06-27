using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    public override void Exit()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        if (_rigidbody.velocity.y < 0)
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
    }

    void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _playerSO.jumpForce, ForceMode2D.Impulse);
    }
}
