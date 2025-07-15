using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.IdleParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.IdleParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(_input.MoveInput.x) > 0f)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
    }

}
