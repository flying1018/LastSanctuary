using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void HandleInput()
    {

    }

    public override void Update()
    {
        if (_input.IsGround())
        {
            CharacterManager.Instance.isJump = false;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {

    }
}
