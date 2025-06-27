using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : MonoBehaviour, IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _input;
    protected PlayerSO _playerSO;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected GameObject _playerModel;
    protected float DashCool = 0.5f;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _input = stateMachine.Player.Input;
        _playerSO = stateMachine.Player.Data;
        _rigidbody = stateMachine.Player.Rigidbody;
        _spriteRenderer = stateMachine.Player.SpriteRenderer;
        _playerModel = stateMachine.Player.Model;

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
        float xDirection = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0;
        Vector2 moveVelocity = new Vector2(xDirection * _playerSO.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction.x != 0)
        {
            _spriteRenderer.flipX = direction.x < 0;
            _playerModel.transform.localPosition = _spriteRenderer.flipX ? 
                new Vector3(Mathf.Abs(_playerModel.transform.localPosition.x), 0, 0) :
                new Vector3(-Mathf.Abs(_playerModel.transform.localPosition.x), 0, 0);
            
        }

    }
}
