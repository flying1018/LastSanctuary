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
    protected BossStateMachine _stateMachine;
    protected BossSO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected PolygonCollider2D _polygonCollider;
    protected BossCondition _condition;
    protected Boss _boss;
    protected BossWeapon _weapon;
    protected KinematicMove _move;
    
    protected float _time;

    public BossBaseState(BossStateMachine bossStateMachine)
    {
        this._stateMachine = bossStateMachine;
        _boss = _stateMachine.Boss;
        _data = _boss.Data;
        _rigidbody = _boss.Rigidbody;
        _spriteRenderer =_boss.SpriteRenderer;
        _polygonCollider = _boss.PolygonCollider;
        _condition = _boss.Condition;
        _weapon = _boss.BossWeapon;
        _move = _boss.Move;
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
        //공격 쿨타임 체크
        if (_stateMachine.Attack1.CheckCoolTime())
        {
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack1);
        }
        
        if (_stateMachine.Attack2.CheckCoolTime())
        {           
             _stateMachine.Attacks.Enqueue(_stateMachine.Attack2);
        }
        
        //3번째 공격 페이즈 2에서만 사용
        if (_boss.Phase2 && _stateMachine.Attack3.CheckCoolTime())
        {           
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack3);
        }
    }

    public virtual void PhysicsUpdate()
    {
        //콜라이더 형태 변경
        UpdateCollider();
        //ApplyGravity();
    }

  

    //중력
    // public void ApplyGravity()
    // {
    //     if (_boss.IsGrounded())
    //     {
    //         _boss.VerticalVelocity = 0f;
    //     }
    //     else
    //     {
    //         _boss.VerticalVelocity += Physics.gravity.y * Time.deltaTime;
    //     }
    // }
    
    //애니메이션 실행
    protected void StartAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, true);
    }

    //애니메이션 정지
    protected void StopAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, false);
    }

    //콜라이더 형태 변경(폴리곤 콜라이더)
    public void UpdateCollider()
    {
        Sprite sprite = _spriteRenderer.sprite;
        _polygonCollider.pathCount = sprite.GetPhysicsShapeCount();

        for (int i = 0; i < _polygonCollider.pathCount; i++)
        {
            List<Vector2> path = new List<Vector2>();
            sprite.GetPhysicsShape(i, path);
            if (_spriteRenderer.flipX)
            {
                for (int j = 0; j < path.Count; j++)
                {
                    Vector2 point = path[j];
                    point.x *= -1;
                    path[j] = point;
                }
            }
            _polygonCollider.SetPath(0, path.ToArray());
        }
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
        _boss.Weapon.transform.rotation = Quaternion.Euler(angle, 0, angle);
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
