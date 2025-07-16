using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어가 점프했을 때 상태
/// </summary>
public class PlayerJumpState : PlayerAirState
{
    private float _maxHoldTime;
    private bool _keyHold;
    
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.JumpParameterHash);

        //데이터 초기화
        _input.IsJump = false;
        _maxHoldTime = 0.2f;
        _keyHold = _input.IsLongJump;
        
        //효과음 실행
        PlaySFX1();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.JumpParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
        //계속 누르고 있으면 점프 지속
        if (!_input.IsLongJump)
            _keyHold = _input.IsLongJump;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Jump();
    }
    
    //점프
    //키를 누르고 있는 동안 점프력 증가
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

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX( _data.jumpSound);
    }
}
