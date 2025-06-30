using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : IState
{
    protected PlayerStateMachine _stateMachine;
    protected PlayerController _input;
    protected PlayerSO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected PlayerCondition _condition;
    protected GameObject _playerModel;
    protected Player _player;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _player = stateMachine.Player;
        _input = _player.Input;
        _data = _player.Data;
        _rigidbody = _player.Rigidbody;
        _spriteRenderer = _player.SpriteRenderer;
        _playerModel = _player.Model;
        _condition = _player.Condition;

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

        if (_player.IsLadder && Mathf.Abs(_input.MoveInput.y) > 0f)
        {
            _stateMachine.ChangeState(_stateMachine.LadderState);
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
        _player.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _player.Animator.SetBool(animatorHash, false);
    }

    public void Move()
    {
        Move(_input.MoveInput);
        Rotate(_input.MoveInput);
    }

    public void Move(Vector2 direction)
    {
        float xDirection = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0;
        Vector2 moveVelocity = new Vector2(xDirection * _data.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction.x != 0)
        {
            //모델 회전
            _spriteRenderer.flipX = direction.x < 0;
            //무기 회전
            float angle = _spriteRenderer.flipX ? 180 : 0;
            _player.Weapon.transform.localRotation = Quaternion.Euler(angle, 0, angle);
        }

    }
}
