using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 dir;
    private float _elapsedTime;
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        _input.IsDash = false;
        _elapsedTime = 0;
        dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
    }

    public override void Exit()
    {
        base.Exit();
        
        _rigidbody.velocity = Vector2.zero;
    }

    public override void HandleInput()
    {

    }

    public override void Update()
    {
        base.Update();

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime >= _data.dashTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        _rigidbody.velocity = dir * _data.dashPower;
    }
    


}
