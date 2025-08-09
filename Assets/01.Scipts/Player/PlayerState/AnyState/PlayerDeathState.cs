using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        _player.Animator.SetTrigger(_player.AnimationDB.DieParameterHash);

        PlaySFX1();

        //입력 막기
        _player.PlayerInput.enabled = false;
        UIManager.Instance.DeathText(4f);
        UIManager.Instance.Fade(3f);

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


    }

    //모든 조작 및 물리 상태 막기
    public override void HandleInput() { }

    public override void Update()
    {
        //죽음 시간 이후
        _time += Time.deltaTime;
        if (_time >= _data.deathTime)
        {   //부활 상태로 전환
            _stateMachine.ChangeState(_stateMachine.RespawnState);

        }
    }

    public override void Exit()
    {

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
