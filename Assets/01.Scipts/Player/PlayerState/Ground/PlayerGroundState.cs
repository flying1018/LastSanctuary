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

        InputDash();

        //가드
        if (_input.IsGuarding)
        {
            _stateMachine.ChangeState(_stateMachine.GuardState);
        }

        //궁극기
        if (_input.IsUltimate && (_condition.CurUltimate >= _condition.MaxUltimate))
        {
            _stateMachine.ChangeState(_stateMachine.UltState);
        }
        
        //아래 키 입력 시
        if (_input.MoveInput.y < 0 && _input.IsJump)
        {
            if (!_move.IsAerialPlatform) return;
            _stateMachine.ChangeState(_stateMachine.DownJumpState);
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

        InputExecution();

        //위 키를 누르고 공격 입력 시
        if (_input.MoveInput.y > 0 && _input.IsAttack)
        {
            _stateMachine.ChangeState(_stateMachine.TopAttackState);
        }

        //공격
        if (_input.IsAttack)
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.ComboAttack[0]);
        }

        //상호작용 키 입력 시 
        if (_player.InteractableTarget != null && _input.IsInteract)
        {
            _stateMachine.ChangeState(_stateMachine.InteractState);
        }
    }

    public void InputExecution()
    {
        if (_player.FindTarget() && _input.IsGroggyAttack)
        {
            //보스에게 그로기 어택
            if (_player.Target.TryGetComponent(out Boss boss))
            {
                if (boss.StateMachine.currentState is BossGroggyState)
                    _stateMachine.ChangeState(_stateMachine.GroggyAttackState);
            }
            
            //스킬 해금 시 몬스터에게도 사용 가능
            if(!_skill.GetSkill(Skill.Execution).open) return;
            if (_player.Target.TryGetComponent(out Enemy enemy))
            {
                if (enemy.StateMachine.currentState is EnemyGroggyState)
                    _stateMachine.ChangeState(_stateMachine.GroggyAttackState);
            }
        }
    }
}
