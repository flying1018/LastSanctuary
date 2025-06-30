using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundState
{ 
    private float _healTimer;
    
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationDB.HealParameterHash); //힐 애니메이션 시작

        _healTimer = _playerSO.HealDuration;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationDB.HealParameterHash);
    }

    public override void HandleInput()
    {
        
        if (!_input.IsHeal)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void Update()
    {
        _healTimer -= Time.deltaTime;

        if (_healTimer <= 0f) //만약 힐 타임이 0보다 작으면
        {
            CompleteHeal(); //실행
        }
    }

    public override void PhysicsUpdate()
    {

    }
    
    private void CompleteHeal()
    {
        //체력+힐값 적기.
        _stateMachine.ChangeState(_stateMachine.IdleState); //힐이 끝나면 아이들 스테이트로
    }
}
