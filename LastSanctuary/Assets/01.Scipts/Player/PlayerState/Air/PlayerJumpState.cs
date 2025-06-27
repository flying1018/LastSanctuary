using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationDB.JumpParameterHash);
        
        Jump();
    }

    public override void Exit()
    {
        base.Enter();
        StopAnimation(_stateMachine.Player.AnimationDB.JumpParameterHash);
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
        _input.IsJump = false;
        _rigidbody.AddForce(Vector2.up * _playerSO.jumpForce, ForceMode2D.Impulse);
    }
}
