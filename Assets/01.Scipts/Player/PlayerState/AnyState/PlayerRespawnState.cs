using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawnState : PlayerBaseState
{
    public PlayerRespawnState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        //세이브 포인트로 위치 변경
        _player.gameObject.transform.position = SaveManager.Instance.GetSavePoint();
        
        //애니메이션 실행
        _player.Animator.SetTrigger(_player.AnimationDB.RespawnParameterHash);
    }

    public override void Exit()
    {
        //체력 회복
        _condition.PlayerRecovery();
        
        //애니메이터 초기화
        _player.Animator.Rebind();
        
        //조작 가능
        _player.PlayerInput.enabled = true;
    }

    public override void HandleInput() { }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time >= _data.respawnTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    public override void PhysicsUpdate() { }
}
