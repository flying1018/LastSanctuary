using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _input;
    protected PlayerSO _playerSO;
    protected Rigidbody2D _rigidbody;
    
    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _input = stateMachine.Player.Input;
        _playerSO = stateMachine.Player.Data;
        _rigidbody = stateMachine.Player.Rigidbody;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {

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
        //Rotate(_input.MoveInput);
    }
    
    public void Move(Vector2 direction)
    {
        Vector2 moveVelocity = new Vector2(direction.normalized.x * _playerSO.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        if(direction != Vector2.zero)
        {
            
        }
    }

    public void Guard()
    {
        //PerfectGuard
        if (_input.IsGuarding)
        {
            Debug.Log("가드 성공");
        }
    }


}
