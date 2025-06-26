using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();

        if (_input.DashTriggered)
        {
            _input.ResetDashTrigger();                       // 한 번만 사용되도록 리셋
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }

        if (CharacterManager.Instance.isJump)
        {
            DebugHelper.Log("점프감지");
            _stateMachine.ChangeState(_stateMachine.JumpState); 
        }
    }

    public override void Update()
    {
        base.Update();

        if (Mathf.Abs(_input.MoveInput.x) > 0f)
        {
            _stateMachine.ChangeState(_stateMachine.MoveState);
        }
    }

    public override void PhysicsUpdate()
    {
        Move(Vector2.zero);
    }

}
