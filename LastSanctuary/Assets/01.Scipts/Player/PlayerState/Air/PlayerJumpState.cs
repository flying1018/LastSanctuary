using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{
    public PlayerJumpState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.playerAnimationDB.JumpParameterHash);

        Rigidbody2D rb = stateMachine.Player.rb;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * playerSO.jumpForce, ForceMode2D.Impulse);
    }

    public override void Exit()
    {
        base.Enter();
        StopAnimation(stateMachine.Player.playerAnimationDB.JumpParameterHash);
    }

    public override void HandleInput()
    {

    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }
}
