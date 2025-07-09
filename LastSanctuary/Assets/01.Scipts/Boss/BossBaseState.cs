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
        _boss = _stateMachine.Boss;
        _data = _boss.Data;
        _rigidbody = _boss.Rigidbody;
        _spriteRenderer =_boss.SpriteRenderer;
        _condition = _boss.Condition;
        this._stateMachine = bossStateMachine;
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

    public virtual  void Update()
    {

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
}
