using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttackState : PlayerAttackState
{
    public DashAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }

    public override void PhysicsUpdate()
    {
        
    }
    
    public bool UseCanDashAttack()
    {
        if (_condition.UsingStamina(StaminaCost)) { return true; }
        else {return false;}
    }
    
    public void CostDown(int cost)
    {
        StaminaCost = cost;
    }
}
