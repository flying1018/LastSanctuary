using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerBaseState
{
    private Vector2 _dir;
    private float _dashElapsedTime; 
    
    public PlayerDashState(PlayerStateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        _dir = _stateMachine.Player.Input.MoveInput.normalized;

        _dashElapsedTime = 0f;
    }

    public override void Exit()
    {

    }

    public override void HandleInput()
    {

    }

    public override void Update()
    {
      
    }

    public override void PhysicsUpdate()
    {
        var rb = _stateMachine.Player.Rigidbody;
        float speed = _stateMachine.Player.Data.dashSpeed;
        rb.velocity = new Vector2(_dir.x * speed, rb.velocity.y);
    }
}
