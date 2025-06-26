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
    
    public override void HandleInput()
    {
        base.HandleInput();

        if (_input.DashTriggered)
        {
            _input.ResetDashTrigger();                       // 한 번만 사용되도록 리셋
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
    }
}
