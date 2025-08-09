using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDownJumpState : PlayerBaseState
{

    public PlayerDownJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _input.IsJump = false;
        
        _boxCollider.enabled = false;
        
        _move.IsGrounded = false;

        _time = 0;
    }
    
    public override void Exit()
    {
        base.Exit();
        
        _boxCollider.enabled = true;
    }
}
