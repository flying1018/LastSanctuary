using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 보스 상태의 부모인 클래스
/// </summary>
public class BossBaseState : IState
{
    //필요하거나 자주쓰는 컴포넌트
    protected BossStateMachine _stateMachine1;
    protected BossSO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider;
    protected BossCondition _condition;
    protected Boss _boss;
    protected BossWeapon _weapon;
    protected KinematicMove _move;
    protected Animator _animator;

    protected Boss02 _boss2;
    protected Boss02StateMachine _stateMachine2;
    protected Boss02Event _boss02Event;
    protected Boss02Condition _condition2;
    
    protected float _time;
    

    public BossBaseState(BossStateMachine bossStateMachine)
    {
        this._stateMachine1 = bossStateMachine;
        _boss = _stateMachine1.Boss;
        _data = _boss.Data;
        _rigidbody = _boss.Rigidbody;
        _spriteRenderer =_boss.SpriteRenderer;
        _boxCollider = _boss.BoxCollider;
        _condition = _boss.Condition;
        _weapon = _boss.BossWeapon;
        _move = _boss.Move;
        _animator = _boss.Animator;

    }

    public BossBaseState(Boss02StateMachine bossStateMachine)
    {
        this._stateMachine2 = bossStateMachine;
        _boss2 = _stateMachine2.Boss;
        _data = _boss2.Data;
        _rigidbody = _boss2.Rigidbody;
        _spriteRenderer =_boss2.SpriteRenderer;
        _boxCollider = _boss2.BoxCollider;
        _condition2 = _boss2.Condition2;
        _weapon = _boss2.BossWeapon;
        _move = _boss2.Move;
        _boss02Event = _boss2.Boss02Event;
        _animator = _boss2.Animator;

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
    
    //애니메이션 실행
    protected void StartAnimation(int animatorHash)
    {
        _animator.SetBool(animatorHash, true);
    }

    //애니메이션 정지
    protected void StopAnimation(int animatorHash)
    {
        _animator.SetBool(animatorHash, false);
    }
    
    //좌우 이동
    public void Move(Vector2 direction)
    {
        Vector2 x = _move.Horizontal(direction, _data.moveSpeed);
        
        if(!_move.IsGrounded)
            _move.gravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        else
            _move.gravityScale = Vector2.zero;
        _move.Move(x + _move.gravityScale);
    }

    //보스 회전
    public void Rotate(Vector2 direction)
    {
        _spriteRenderer.flipX = direction.x < 0; //보스의 방향
        
        //무기 회전
        float angle = _spriteRenderer.flipX ? 180 : 0;
        _weapon.gameObject.transform.rotation = Quaternion.Euler(angle, 0, angle);
    }
    
    //추적 거리 안에 들어오는지 확인하는 메서드
    protected bool WithinChaseDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange/2;
    }

    //공격 거리 안에 들어오는지 확인하는 메서드
    protected bool WithAttackDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange;
    }
    
    //플레이어의 방향을 구하는 메서드
    public Vector2 DirectionToTarget()
    {
        if(_boss.Target == null) return Vector2.zero; //방어코드
        return (_boss.Target.position - _boss.transform.position).normalized; //플레이어 방향
    }
    
    public virtual void PlayEvent1() { }
    public virtual void PlayEvent2() { }
    public virtual void PlayEvent3() { }
    
    public virtual void PlaySFX1() { }
    public virtual void PlaySFX2() { }
    public virtual void PlaySFX3() { }
}
