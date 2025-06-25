using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerBaseState
{
    public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    

    public override void Update()
    {
        base.Update();

        if (_input.MoveInput.x == 0f) 
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
    
    public override void PhysicsUpdate()
    {
        Move();
    }
}
