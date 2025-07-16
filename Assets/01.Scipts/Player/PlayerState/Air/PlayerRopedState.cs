using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopedState : PlayerAirState
{
    public PlayerRopedState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.RopedParameterHash);;
        
        _rigidbody.gravityScale = 0;
        
        //사운드 추가
        SoundClip[0] = _data.ropeSound;
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.RopedParameterHash);;
        
        _rigidbody.gravityScale = 3;
    }

    
    public override void HandleInput()
    {
        //좌우 입력 중이고, 대쉬 키를 입력하면
        if (Mathf.Abs(_input.MoveInput.x) > 0 && 
            _input.IsDash && _condition.UsingStamina(_data.dashCost))
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState);
        }

        //좌우 입력 중이고, 점프 키를 입력하면 
        if (_input.IsJump && Mathf.Abs(_input.MoveInput.x) > 0)
        {   //점프
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        //아래키 입력 중이고 공중 발판 이 있을 때 공중 발판 뚫기
        if (_input.MoveInput.y < 0)
        {
            if (_player.AerialPlatform == null) return;
            _player.AerialPlatform.DownJump();
        }

        //아래키 입력 중이고 바닥에 도달했을 때
        if (_input.MoveInput.y < 0 && _player.IsGround())
        {
            if(_player.IsAerialPlatform()) return;
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
        VerticalMove(_input.MoveInput);
    }

    //상하 이동
    void VerticalMove(Vector2 direction)
    {
        //로프 x 좌표 고정
        if(_player.RopedPosition == Vector2.zero) return;
        
        //x 축 좌표 고정
        float ropeX = _player.RopedPosition.x + (_spriteRenderer.flipX ? +_capsuleCollider.size.x / 2 : -_capsuleCollider.size.x / 2); 
        _player.transform.position = new Vector2(ropeX, _player.transform.position.y);
        
        //y 축 좌표 고정
        direction.x = 0;
        Vector2 moveVelocity = new Vector2(_rigidbody.velocity.x, direction.normalized.y * _data.moveSpeed);
        _rigidbody.velocity = moveVelocity;
    }
}
