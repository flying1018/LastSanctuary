using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        if (_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
        
        if (_input.IsJump)
        {
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }

        if (_input.IsHeal)
        {
            _stateMachine.ChangeState(_stateMachine.HealState);
        }
    }
}
