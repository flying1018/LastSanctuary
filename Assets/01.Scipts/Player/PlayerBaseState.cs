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
    protected BoxCollider2D _boxCollider;
    protected PlayerKinematicMove _move;
    protected PlayerCamera _camera;
    protected PlayerAttackSO _attackData;
    protected PlayerSkill _skill;

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
        _boxCollider = _player.BoxCollider;
        _inventory = _player.Inventory;
        _move = _player.Move;
        _camera = _player.Camera;
        _attackData = _player.AttackData;
        _skill = _player.Skill;
    }

    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {
        //로프에 닿고, 상하 키 입력 시
        if (_player.IsRoped && Mathf.Abs(_input.MoveInput.y) > 0f)
        {   //로프 상태
            _stateMachine.ChangeState(_stateMachine.RopeMoveState);
        }
    }

    public virtual void Update()
    {
        //스태미나 회복
        _condition.RecoveryStamina();
        
        //떨어지기 시작하면
        if (!_move.IsGrounded && _move.GravityScale.y < -_data.fallJudgment)
        {   //떨어지는 상태
            _time = 0;
            _stateMachine.ChangeState(_stateMachine.FallState);
        }
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


    //캐릭터 기초 이동
    public void Move()
    {
        Rotate(_input.MoveInput);
        
        Vector2 x = _move.Horizontal(_input.MoveInput, _data.moveSpeed);
        
        //바닥에 있지 않으면
        if(!_move.IsGrounded)
            _move.GravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        else
            _move.GravityScale = Vector2.zero;
        
        _move.Move(x + _move.GravityScale);
    }

    public void ApplyGravity()
    {
        _move.GravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        _move.Move( _move.GravityScale);
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
        
        //카메라 회전
        _camera.RotateCamera(direction);
    }

    public void InputDash()
    {
        //대쉬 키 입력 시 쿨타임이 돌고 스태미나가 충분하면
        if (_input.IsDash && _stateMachine.DashState.UseCanDash())
        {   //대쉬
            _stateMachine.ChangeState(_stateMachine.DashState); 
        }
    }

    #region AnimationEvent Method

    public virtual void PlayEvent1() { }

    public virtual void PlaySFX1() { }
    public virtual void PlaySFX2() { }
    public virtual void PlaySFX3() { }
    
    #endregion
    
}
