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
    protected BoxCollider2D _capsuleCollider;

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
        if (!_player.IsGrounded)
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


    //캐릭터 기초 이동
    public void Move()
    {
        Rotate(_input.MoveInput);
        Vector2 x = Horizontal(_input.MoveInput, _data.moveSpeed);
        _player.gravityScale += Vertical(Vector2.down, _data.gravityPower);
        Move(x+_player.gravityScale);
    }

    //캐릭터의 이동 처리
    public void Move(Vector2 direction)
    {
        Vector2 horizontal = direction;
        Vector2 vertical = direction;
        
        horizontal.y = 0;
        vertical.x = 0;
        
        if (_player.IsWall &&_player.WallDirection.normalized == horizontal.normalized) direction.x = 0;
        if (_player.IsGrounded && _player.GroundDirection.normalized == vertical.normalized) direction.y = 0;
        
        _rigidbody.MovePosition(_rigidbody.position + direction * Time.fixedDeltaTime);
    }

    public Vector2 Horizontal(Vector2 direction, float power)
    {
        direction.y = 0;
        return direction.normalized * power;
    }

    public Vector2 Vertical(Vector2 direction, float power)
    {
        direction.x = 0;
        return direction.normalized * power;
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
