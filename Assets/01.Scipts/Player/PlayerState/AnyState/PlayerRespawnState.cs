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

        //적 부활
        MapManager.Instance.RespawnMap();
        //포션 개수 회복
        _inventory.SupplyPotion();

        _time = 0;
    }

    public override void Exit()
    {
        //체력 회복
        _condition.PlayerRecovery();
        //리스폰시 저장
        if (_player.InteractableTarget is SavePoint savePoint)
            _player.InteractableTarget.Interact();
        //애니메이터 초기화
        _player.Animator.Rebind();
        //조작 가능
        _player.PlayerInput.enabled = true;
        //무적 종료
        _condition.IsInvincible = false;
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
