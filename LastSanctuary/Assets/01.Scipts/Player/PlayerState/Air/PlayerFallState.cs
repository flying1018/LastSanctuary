using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    public PlayerFallState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate()
    {
        if (_input.IsGround())
        {
            Debug.Log("EndFall");
            _input.IsJump = false;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
