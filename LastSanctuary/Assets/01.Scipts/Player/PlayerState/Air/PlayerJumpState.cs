using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    private float _maxHoldTime;
    private bool _keyHold;
    
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.JumpParameterHash);

        _input.IsJump = false;
        _maxHoldTime = 0.2f;
        _keyHold = _input.IsLongJump;
        
        //사운드
        SoundClip[0] = _data.jumpSound;
        _player.PlaySFX1();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.JumpParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        if (!_input.IsLongJump)
            _keyHold = _input.IsLongJump;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Jump();
    }


    void Jump()
    {
        if (_keyHold && _maxHoldTime > 0f)
        {
            _maxHoldTime -= Time.deltaTime;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _data.jumpForce);
        }
        else
        {
            _keyHold = false;
        }


    }
}
