using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 dir;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        Invoke(nameof(ChangeIdleState), _playerSO.dashTime);
    }

    public override void Exit()
    {
        base.Exit();
        
        _rigidbody.velocity = Vector2.zero;
    }

    public override void HandleInput()
    {

    }

    public override void PhysicsUpdate()
    {
        _rigidbody.velocity = dir * _playerSO.dashPower;
    }

    void ChangeIdleState()
    {
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }
}
