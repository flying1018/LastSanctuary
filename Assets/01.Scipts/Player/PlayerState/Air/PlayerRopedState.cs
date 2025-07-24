using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopedState : PlayerAirState
{
    public PlayerRopedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.RopedParameterHash);

        _move.gravityScale = Vector2.zero;
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.RopedParameterHash);
    }

    
    public override void HandleInput()
    {
        //좌우 입력 중이고, 대쉬 키를 입력하면
        if (Mathf.Abs(_input.MoveInput.x) > 0 && 
            _input.IsDash && _stateMachine.DashState.UseCanDash())
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState);
        }

        //점프 키를 입력하면 
        if (_input.IsJump)
        {   //점프
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }

        //아래키 입력 중이고 공중 발판에 있을 때 공중 발판 뚫기
        if (_input.MoveInput.y < 0)
        {
            if (!_move.IsAerialPlatform) return;
            _move.IsGrounded = false;
        }

        //아래키 입력 중이고 바닥에 도달했을 때
        if (_input.MoveInput.y < 0 && _move.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void Update()
    {
        //스테미나 회복
        base.Update();
        
        //로프에서 탈출 했을 때
        if(!_player.IsRoped)
            _stateMachine.ChangeState(_stateMachine.IdleState);
        
    }
    
    
    public override void PhysicsUpdate()
    {
        Rotate(_input.MoveInput);
        RopeMove();
    }

    //상하 이동
    void RopeMove()
    {
        //로프 x 좌표 고정
        if(_player.RopedPosition == Vector2.zero) return;
        
        //x 축 좌표 고정
        float ropeX = _player.RopedPosition.x + (_spriteRenderer.flipX ? +_boxCollider.size.x / 2 : -_boxCollider.size.x / 2); 
        _player.transform.position = new Vector2(ropeX, _player.transform.position.y);
        
        //y 축 좌표 고정
        Vector2 dir = _move.Vertical(_input.MoveInput, _data.moveSpeed);
        _move.Move(dir);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX( _data.ropeSound);
    }
}
