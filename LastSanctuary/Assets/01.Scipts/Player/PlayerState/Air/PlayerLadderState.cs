using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerAirState
{

    public PlayerLadderState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        base.Enter();
        
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
    }
    
    public override void Exit()
    {
        base.Exit();
        
        _rigidbody.gravityScale = 2;
    }

    public override void HandleInput()
    {
        if (_input.IsDash)
        {
            _stateMachine.ChangeState(_stateMachine.DashState);
        }

        if (_input.IsJump)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }

        if (_input.MoveInput.y < 0)
        {
            if (_player.AerialPlatform == null) return;
            _player.AerialPlatform.DownJump();
        }

    }

    public override void Update()
    {
        if(!_player.IsLadder)
            _stateMachine.ChangeState(_stateMachine.IdleState);
        if (_input.MoveInput.y < 0 && _player.IsGround())
        {
            if(_player.IsAerialPlatform()) return;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    
    
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        VerticalMove(_input.MoveInput);
    }

    void VerticalMove(Vector2 direction)
    {
        float yDirection = direction.y > 0 ? 1 : direction.y < 0 ? -1 : 0;
        Vector2 moveVelocity = new Vector2(_rigidbody.velocity.x, yDirection * _data.moveSpeed);
        _rigidbody.velocity = moveVelocity;
    }
}
