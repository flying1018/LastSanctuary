using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerGroundState
{ 
    public PlayerHealState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationDB.HealParameterHash); //힐 애니메이션 시작
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationDB.HealParameterHash);
    }

    public override void HandleInput()
    {
        
        if (!_input.IsHeal) //만약 힐을 하지 않고있을때
        {
            _stateMachine.ChangeState(_stateMachine.IdleState); //아이들 스테이트로 전환
        }
    }

    public override void Update()
    {
        
    }

    public override void PhysicsUpdate()
    {

    }
}
