using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_player.AnimationDB.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_player.AnimationDB.AirParameterHash);
    }

    public override void HandleInput()
    {
        base.HandleInput();
        if (_input.IsAttack)
        {
            _stateMachine.ChangeState(_stateMachine.JumpAttack);
        }
    }
}
