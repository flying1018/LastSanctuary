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
    protected BossCondition _condition;
    protected Boss _boss;

    public BossBaseState(BossStateMachine bossStateMachine)
    {
        this._stateMachine = bossStateMachine;
        
        _boss = _stateMachine.Boss;
        _data = _boss.Data;
        _rigidbody = _boss.Rigidbody;
        _spriteRenderer =_boss.SpriteRenderer;
        _condition = _boss.Condition;
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
        if(_stateMachine.Attack1.CheckCoolTime())
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack1);
        if(_stateMachine.Attack2.CheckCoolTime())
            _stateMachine.Attacks.Enqueue(_stateMachine.Attack2);
    }

    public virtual void PhysicsUpdate()
    {

    }
    
    protected void StartAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _boss.Animator.SetBool(animatorHash, false);
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
    }
    
    protected bool WithinChaseDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange/10;
    }

    protected bool WithAttackDistance()
    {
        if (_boss.Target == null) return false;

        float distance = Vector2.Distance(_boss.transform.position, _boss.Target.transform.position);
        return distance <= _data.attackRange/10;
    }
}
