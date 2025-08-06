using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02RushAttackState : Boss02AttackState
{
    private Vector2 _rushDir;
    
    public Boss02RushAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

    public override void Enter()
    {
        base.Enter();
        
        _rushDir = (_stateMachine2.TargetMirror - _boss2.transform.position).normalized;
        Rotate(_rushDir);
        
        _boxCollider.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
        
        _boxCollider.enabled = true;
    }


    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(_boss2.transform.position, _stateMachine2.TargetMirror) < 0.2f)
        {
            _stateMachine2.ChangeState(_stateMachine2.IdleState);
        }   
    }
    
    public override void PhysicsUpdate()
    {
        _move.Move(_rushDir * _attackInfo.projectilePower);
    }
}
