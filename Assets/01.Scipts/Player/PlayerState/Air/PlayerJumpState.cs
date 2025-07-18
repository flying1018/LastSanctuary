using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어가 점프했을 때 상태
/// </summary>
public class PlayerJumpState : PlayerAirState
{
    private float _jumpDumping;

    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.JumpParameterHash);

        //데이터 초기화
        _input.IsJump = false;

        //효과음 실행
        PlaySFX1();
        
        //점프
        Jump();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.JumpParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();


        if (_input.IsHoldJump)
            _jumpDumping = _data.holdJumpDuping;
        else
            _jumpDumping = _data.jumpDuping;


    }

    public override void Update()
    {
        //스태미나 회복
        _condition.RecoveryStamina();
        
        //점프가 끝나기 전까진 상태 변환 없음.
        if(_move.addForceCoroutine != null) return;
        base.Update();
    }

    public override void PhysicsUpdate()
    {
        //점프가 끝나기 전까진 상태 변환 없음.
        if(_move.addForceCoroutine != null) return;
        base.PhysicsUpdate();
    }

    //점프
    //키를 누르고 있는 동안 점프력 증가
    void Jump()
    {
        if (_move.addForceCoroutine != null)
        {
            _move.StopCoroutine(_move.addForceCoroutine);
            _move.addForceCoroutine = null;
        }
        
        _move.addForceCoroutine = _move.StartCoroutine(Jump_Coroutine());
    }

    IEnumerator Jump_Coroutine()
    {
        float jumpPower = _data.jumpForce;
        while (jumpPower > 2f)
        {
            Rotate(_input.MoveInput);
            Vector2 hor = _move.Horizontal(_input.MoveInput, _data.moveSpeed);
            Vector2 ver = _move.Vertical(Vector2.up, jumpPower);
            _move.Move(hor + ver);

            yield return null;
            jumpPower *= _jumpDumping;
        }

        _move.addForceCoroutine = null;
    }
    
    
    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX( _data.jumpSound);
    }
}
