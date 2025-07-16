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

    protected float _time;

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
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {
        //대쉬 키 입력 시 스태미나가 충분하면
        if (_input.IsDash && _condition.UsingStamina(_data.dashCost))
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState); 
        }
        
        //로프에 닿고, 상하 키 입력 시
        if (_player.IsRoped && Mathf.Abs(_input.MoveInput.y) > 0f)
        {   //로프 상태
            _stateMachine.ChangeState(_stateMachine.RopedState);
        }
    }

    public virtual void Update()
    {
        //스태미나 회복
        _condition.RecoveryStamina();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
        
        //떨어지기 시작하면
        if (!_player.IsGround()&&_rigidbody.velocity.y < -1f)
        {   //떨어지는 상태
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

    //입력 값에 이동하는 메서드
    public void Move()
    {
        Move(_input.MoveInput);
        Rotate(_input.MoveInput);
    }

    public void Move(Vector2 direction)
    {
        direction.y = 0;
        Vector2 moveVelocity = new Vector2(direction.normalized.x * _data.moveSpeed, _rigidbody.velocity.y);
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
            _player.Weapon.transform.rotation = Quaternion.Euler(angle, 0, angle);
            
        }
    }

    public virtual void PlaySFX1() { }
    public virtual void PlaySFX2() { }
}
