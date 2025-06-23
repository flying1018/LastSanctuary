using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.playerAnimationDB.MoveParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.playerAnimationDB.MoveParameterHash);
    }

    public override void HandleInput()
    {

    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
