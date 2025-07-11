using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.GroundParameterHash);
    }   

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.GroundParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
        
        if (_input.IsJump && _player.IsGround())
        {
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }


        if (_input.IsHeal && _inventory.CurPotionNum > 0)
        {
            _stateMachine.ChangeState(_stateMachine.HealState);
        }
        
        if (_input.IsAttack)
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.ComboAttack[0]);
        }

        if (_input.MoveInput.y < 0)
        {
            if(_player.AerialPlatform == null) return;
            _player.AerialPlatform.DownJump();
        }
    }
}
