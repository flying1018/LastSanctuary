using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _input;
    protected PlayerSO _playerSO;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected PlayerCondition _condition;
    protected float _elapsedTime;
    protected float DashCool = 0.5f;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _input = stateMachine.Player.Input;
        _playerSO = stateMachine.Player.Data;
        _rigidbody = stateMachine.Player.Rigidbody;
        _spriteRenderer = stateMachine.Player.SpriteRenderer;
        _condition = stateMachine.Player.Condition;

    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {
        if (_input.IsDash)
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }
    }

    public virtual void Update()
    {
        
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    protected void StartAnimation(int animatorHash)
    {
        _stateMachine.Player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _stateMachine.Player.Animator.SetBool(animatorHash, false);
    }

    public void Move()
    {
        Move(_input.MoveInput);
        Rotate(_input.MoveInput);
    }

    public void Move(Vector2 direction)
    {
        Vector2 moveVelocity = new Vector2(direction.normalized.x * _playerSO.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            _spriteRenderer.flipX = direction.x < 0;
        }
    }
}
