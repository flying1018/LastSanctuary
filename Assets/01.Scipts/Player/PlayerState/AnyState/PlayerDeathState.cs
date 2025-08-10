using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    private bool _fadeIn;
    private bool _deathText;
    
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _player.Animator.SetTrigger(_player.AnimationDB.DieParameterHash);

        PlaySFX1();

        //입력 막기
        _player.PlayerInput.enabled = false;

        //시간 체크
        _time = 0;

        //무적 처리
        _condition.IsInvincible = true;

        //물리 없애기
        if (_move.AddForceCoroutine != null)
        {
            _move.StopCoroutine(_move.AddForceCoroutine);
            _move.AddForceCoroutine = null;
        }

        _fadeIn = false;

    }

    //모든 조작 및 물리 상태 막기
    public override void HandleInput() { }

    public override void Update()
    {
        //죽음 시간 이후
        _time += Time.deltaTime;
        if (_time >= _data.deathTime && !_fadeIn)
        {   //부활 상태로 전환
            _fadeIn = true;
            UIManager.Instance.FadeIn(0,Color.black,0,1.5f);
        }

        if (_time >= _data.deathTime + 1.5f && !_deathText)
        {
            _deathText = true;
            //세이브 포인트로 위치 변경
            _move.gravityScale = Vector2.zero;
            _player.gameObject.transform.position = SaveManager.Instance.GetSavePoint();
            UIManager.Instance.DeathText(2f);
        }

        if (_time >= _data.deathTime + 3.5f)
        {
            _stateMachine.ChangeState(_stateMachine.RespawnState);       
        }
        
        
    }

    public override void Exit()
    {
        _move.gravityScale = Vector2.zero;
    }

    public override void PhysicsUpdate()
    {
        ApplyGravity();
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.deathSound);
    }
}
