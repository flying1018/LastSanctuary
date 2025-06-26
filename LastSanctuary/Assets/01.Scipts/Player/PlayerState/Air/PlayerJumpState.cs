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
    }

    public override void Exit()
    {
        base.Enter();
    }

    public override void PhysicsUpdate()
    {
        if (_rigidbody.velocity.y < 0)
        {
            Debug.Log("EndJump");
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
    }
}
