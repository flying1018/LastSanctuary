using System.Collections;
using System.Collections.Generic;
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

    public virtual  void Update()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
