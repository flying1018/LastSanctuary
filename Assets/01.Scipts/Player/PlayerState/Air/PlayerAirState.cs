using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 공중에 있을 때 상태
/// </summary>
public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.AirParameterHash);
        
        _move.IsGrounded = false;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.AirParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        InputAirDash();
        
        if (_input.MoveInput.y >0 && _input.IsAttack && _stateMachine.JumpTopAttack.CanJumpAttack)
        {
            _stateMachine.ChangeState(_stateMachine.JumpTopAttack);
        }
        
        if (_input.IsAttack && _stateMachine.JumpAttack.CanJumpAttack)
        {
            _stateMachine.ChangeState(_stateMachine.JumpAttack);
        }
        
    }

    public override void Update()
    {
        base.Update();
        if (_move.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);      
        }
    }
    
    public void InputAirDash()
    {
        if(!_skill.GetSkill(Skill.AirDash).open)return;
        InputDash();
    }

}
