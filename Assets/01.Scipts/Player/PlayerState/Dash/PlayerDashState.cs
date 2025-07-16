using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 _dir;
    private float _time;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _player.Animator.SetTrigger(_player.AnimationDB.DashParameterHash);
        
        _input.IsDash = false;
        _time = 0;
        
        //입력이 있는지 없는지에 따라 방향 설정
        if (_input.MoveInput.x == 0)
            _dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        else
            _dir = _input.MoveInput.x < 0 ? Vector2.left : Vector2.right;
        Rotate(_dir);
        
        //효과음
        PlaySFX1();
    }

    public override void Exit()
    {
        base.Exit();
        
        //현재 위치에 정지
        Move(Vector2.zero);
    }

    public override void HandleInput()
    {
        if (_input.IsAttack)
        {
            int cost = _stateMachine.DashAttack.attackInfo.staminaCost;
            //스테미나가 충분하다면 대쉬 공격
            if (_condition.UsingStamina(cost))
            {
                _stateMachine.ChangeState(_stateMachine.DashAttack);
            }
        }
    }

    public override void Update()
    {
        //대쉬가 끝나면
        _time += Time.deltaTime;
        if (_time < _data.DashTime) return;

        //공중이라면 
        if (!_player.IsGround())
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

    //대쉬
    private void Dash()
    {
        _rigidbody.velocity = _dir * _data.dashPower;
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.dashSound);
    }
}
