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
    protected PlayerInventory _inventory;
    protected Player _player;
    protected PlayerWeapon _playerWeapon;
    protected CapsuleCollider2D _capsuleCollider;

    public AudioClip[] SoundClip;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this._stateMachine = stateMachine;
        _player = stateMachine.Player;
        _input = _player.Input;
        _data = _player.Data;
        _rigidbody = _player.Rigidbody;
        _spriteRenderer = _player.SpriteRenderer;
        _condition = _player.Condition;
        _playerWeapon = _player.PlayerWeapon;
        _capsuleCollider = _player.CapsuleCollider;
        _inventory = _player.Inventory;

        SoundClip = new AudioClip[10];
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {
            _stateMachine.ChangeState(_stateMachine.DashState); // 대시 상태로 전환
        }

        if (_player.IsRoped && Mathf.Abs(_input.MoveInput.y) > 0f)
        {
            _stateMachine.ChangeState(_stateMachine.RopedState);
        }
    }

    public virtual void Update()
    {
        _condition.RecoveryStamina();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
        _player.Handler.ApplyGravity();


        if (!_player.IsGround() && _rigidbody.velocity.y < -1f)
        {
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
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

        Vector2 targetPosition = _player.Rigidbody.position + _player.Handler.velocity * Time.fixedDeltaTime;

        _player.Rigidbody.MovePosition(targetPosition);
    }

    public void Move(Vector2 direction)
    {
        _player.Handler.velocity.x = direction.x * _data.moveSpeed;
    }

    public void Rotate(Vector2 direction)
    {
        if (direction.x != 0)
        {
            //모델 회전
            _spriteRenderer.flipX = direction.x < 0;
            //무기 회전
            float angle = _spriteRenderer.flipX ? 180 : 0;
            _player.Weapon.transform.rotation = Quaternion.Euler(angle, 0, angle);

        }
    }
}
