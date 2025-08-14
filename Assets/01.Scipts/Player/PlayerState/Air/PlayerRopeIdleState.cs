using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeIdleState : PlayerAirState
{
    public PlayerRopeIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.RopeIdleParameterHash);
        
        _move.GravityScale = Vector2.zero;
    }
    
    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.RopeIdleParameterHash);
    }

    
    public override void HandleInput()
    {
        //대쉬 키를 입력하면
        if (_input.IsDash && _stateMachine.DashState.UseCanDash())
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState);
        }

        //점프 키를 입력하면 
        if (_input.IsJump)
        {   //점프
            _stateMachine.ChangeState(_stateMachine.JumpState);
        }
        
        //위아래 입력이 있으면
        if (_input.MoveInput.y != 0)
        {
            _stateMachine.ChangeState(_stateMachine.RopeMoveState);
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
        RopeIdle();
    }

    //상하 이동
    protected void RopeIdle()
    {
        Rotate(_input.MoveInput);
        
        //로프 x 좌표 고정
        if(_player.RopedPosition == Vector2.zero) return;
        
        //x 축 좌표 고정
        float ropeX = _player.RopedPosition.x + (_spriteRenderer.flipX ? +_boxCollider.size.x / 2 : -_boxCollider.size.x / 2); 
        _player.transform.position = new Vector2(ropeX, _player.transform.position.y);
    }
}
