using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeMoveState : PlayerRopeIdleState
{
    public PlayerRopeMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        StartAnimation(_player.AnimationDB.AirParameterHash);
        StartAnimation(_player.AnimationDB.RopeMoveParameterHash);
        
        _move.GravityScale = Vector2.zero;
    }
    
    public override void Exit()
    {
        StopAnimation(_player.AnimationDB.AirParameterHash);
        StopAnimation(_player.AnimationDB.RopeMoveParameterHash);
    }

    
    public override void HandleInput()
    {
        base.HandleInput();

        if (_input.MoveInput.y == 0)
        {
            _stateMachine.ChangeState(_stateMachine.RopeIdleState);       
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
    
    public override void PhysicsUpdate()
    {
        RopeMove();
    }

    //상하 이동
    void RopeMove()
    {
        RopeIdle();
        
        //y 축 좌표 고정
        Vector2 dir = _move.Vertical(_input.MoveInput, _data.moveSpeed);
        _move.Move(dir);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX( _data.ropeSound);
    }
}
