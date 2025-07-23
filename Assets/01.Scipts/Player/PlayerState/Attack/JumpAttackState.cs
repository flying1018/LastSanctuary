using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : PlayerAttackState
{
    public bool CanJumpAttack;
    public JumpAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
        CanJumpAttack = true;
    }

    public override void Enter()
    {
        base.Enter();

        CanJumpAttack = false;
    }

    public override void Update()
    {
        if (_move.IsGrounded)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        base.Update();
        
    }

    public override void PhysicsUpdate()
    {
        //점프 유지
        if(_move.addForceCoroutine != null) return;

        //점프가 끝나면 떨어지기
        ApplyGravity();
    }
}
