using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 _dir;
    private bool _isDashCool;

    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        _isDashCool = true;
    }

    public override void Enter()
    {
        base.Enter();
        
        _player.Animator.SetTrigger(_player.AnimationDB.DashParameterHash);
        
        //데이터 초기화
        _input.IsDash = false;
        _time = 0;
        
        //쿨타임 체크
        _player.StartCoroutine(CoolTime_Coroutine());
        
        //효과음
        PlaySFX1();
    }

    public override void Exit()
    {
        base.Exit();
        _move.gravityScale = Vector2.zero;
    }

    public override void HandleInput()
    {
        if (_time < 0.1) SetDashDir();
        
        InputDashAttack();
    }

    public override void Update()
    {
        //대쉬가 끝나면
        _time += Time.deltaTime;
        if (_time < _data.DashTime) return;

        //공중이라면 
        if (!_move.IsGrounded)
        {   //떨어지기
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
        //입력 값이 있으면
        else if (_input.MoveInput.x != 0)
        {   //이동
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
        //입력 값이 없으면
        else if (_input.MoveInput.x == 0)
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Dash();
    }
    
    public void SetDashDir()
    {
        //입력이 있는지 없는지에 따라 방향 설정
        if (_input.MoveInput.x == 0)
            _dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        else
            _dir = _input.MoveInput.x < 0 ? Vector2.left : Vector2.right;
        
        Rotate(_dir);
    }

    //대쉬
    private void Dash()
    {
        Vector2 hor = _move.Horizontal(_dir, _data.dashPower);
        _move.Move(hor);
    }

    private void InputDashAttack()
    {
        //스킬 오픈 여부
        if(!_skill.GetSkill(Skill.DashAttack).open) return;
        if (_input.IsAttack)
        {
            //스테미나가 충분하다면 대쉬 공격
            if (_stateMachine.DashAttack.UseCanDashAttack())
            {
                _stateMachine.ChangeState(_stateMachine.DashAttack);
            }
        }
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.dashSound);
    }
    
    public bool UseCanDash()
    {
        if (_isDashCool && _condition.UsingStamina(_data.dashCost))
            return true;
        else
            return false;
            
        
    }

    IEnumerator CoolTime_Coroutine()
    {
        _isDashCool = false;
        yield return new WaitForSeconds(_data.dashCoolTime);
        _isDashCool = true;
    }
}
