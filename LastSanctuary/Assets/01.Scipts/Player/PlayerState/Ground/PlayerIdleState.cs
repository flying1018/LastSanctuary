using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        DebugHelper.Log("아이들 상태 진입");
        base.Enter();
        StartAnimation(stateMachine.Player.playerAnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        DebugHelper.Log("아이들 상태 해제");
        base.Exit();
        StopAnimation(stateMachine.Player.playerAnimationDB.IdleParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
