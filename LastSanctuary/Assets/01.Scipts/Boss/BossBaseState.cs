using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBaseState : IState
{
    protected BossStateMachine _stateMachine;
    protected BossSO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected PolygonCollider2D _polygonCollider;
    protected BossCondition _condition;
    protected Boss _boss;
    protected BossWeapon _weapon;

    public List<AudioClip> SoundClip;

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
        SoundClip = new List<AudioClip>();
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
        if (_stateMachine.Attack1.CheckCoolTime())
        {
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack1);
        }
        
        if (_stateMachine.Attack2.CheckCoolTime())
        {           
             _stateMachine.Attacks.Enqueue(_stateMachine.Attack2);
        }
        
        if (_boss.Phase2 && _stateMachine.Attack3.CheckCoolTime())
        {           
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack3);
        }
      
    }

    public virtual void PhysicsUpdate()
    {
        UpdateCollider();
    }
    
    protected void StartAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, false);
    }

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
    
    public void Move(Vector2 direction)
    {
        float xDirection = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0; //보스 좌우 움직임
        Vector2 moveVelocity = new Vector2(xDirection * _data.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    public void Rotate(Vector2 direction)
    {
        _spriteRenderer.flipX = direction.x < 0; //보스의 방향
        
        //무기 회전
        float angle = _spriteRenderer.flipX ? 180 : 0;
        _boss.Weapon.transform.rotation = Quaternion.Euler(angle, 0, angle);
    }
    
    protected bool WithinChaseDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange/2;
    }

    protected bool WithAttackDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange;
    }
}
