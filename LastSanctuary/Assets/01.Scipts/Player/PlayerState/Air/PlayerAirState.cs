using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(_stateMachine.Player.AnimationDB.AirParameterHash);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(_stateMachine.Player.AnimationDB.AirParameterHash);
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
