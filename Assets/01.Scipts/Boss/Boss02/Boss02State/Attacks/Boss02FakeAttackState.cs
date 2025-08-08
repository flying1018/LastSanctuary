using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02FakeAttackState : Boss02AttackState
{
    public Boss02FakeAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }
    
    public override void Enter()
    {
        base.Enter();
        
        _condition2.DontCollision = true;
    }
    
    public override void Exit()
    {
        base.Exit();


        _condition2.DontCollision = false;
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
