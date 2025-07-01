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


    void Jump()
    {
        _input.IsJump = false;
        _rigidbody.AddForce(Vector2.up * _data.jumpForce, ForceMode2D.Impulse);
    }
}
