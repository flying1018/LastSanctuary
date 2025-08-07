using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02FakeAttackState : Boss02AttackState
{
    public Boss02FakeAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }
    
    public override void Enter()
    {
        base.Enter();
        
        _boxCollider.enabled = false;
    }
    
    public override void Exit()
    {
        base.Exit();
        
        _boxCollider.enabled = true;
        _move.gravityScale = Vector2.zero;
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime)
        {
            _stateMachine2.ChangeState(_stateMachine2.JugFakeDown);       
        }
    }

    public override void PhysicsUpdate()
    {
        Move(Vector2.down * _attackInfo.projectilePower);
    }
}
