using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.FallParameterHash);;
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.FallParameterHash);;
    }

    public override void Update()
    {
        base.Update();
        
        if (_player.IsGround())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
