using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationDB.FallParameterHash);;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationDB.FallParameterHash);;
    }

    public override void Update()
    {
        if (_player.IsGround())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
