using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerBaseState
{
    public PlayerGroundState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.GroundParameterHash);

        //착지하면 점프 어택 초기화
        _stateMachine.JumpAttack.CanJumpAttack = true;
    }   

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.GroundParameterHash);
    }

    public override void HandleInput()
    {
        //대쉬와 로프에 매달리기 가능
        base.HandleInput();
        
        //가드
        if (_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }
        
        //점프
        if (_input.IsJump && _move.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }

        //현재 포션이 충분하면 힐
        if (_input.IsHeal && _inventory.CurPotionNum > 0)
        {
            _stateMachine.ChangeState(_stateMachine.HealState);
        }
        
        //공격
        if (_input.IsAttack)
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.ComboAttack[0]);
        }
        
        //아래 키 입력 시
        if (_input.MoveInput.y < 0)
        {
            if(!_move.IsAerialPlatform) return;
            _move.IsGrounded = false;
        }

        if (_player.InteractableTarget != null && _input.IsInteract)
        {
            _stateMachine.ChangeState(_stateMachine.InteractState);
        }
    }
}
