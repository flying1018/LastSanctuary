using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackState : PlayerAttackState
{
    public DashAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }

    public override void PhysicsUpdate()
    {
        
    }
}
