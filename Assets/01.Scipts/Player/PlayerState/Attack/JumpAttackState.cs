using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : PlayerAttackState
{
    public JumpAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
    }
    
    

    public override void PhysicsUpdate()
    {
        //점프 유지
        if(_move.addForceCoroutine != null) return;

        //점프가 끝나면 떨어지기
        ApplyGravity();
    }
}
