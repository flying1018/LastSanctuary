using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongAttackState : PlayerAttackState
{
    public StrongAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }

    public bool UseCanStrongAttack()
    {
        if (_condition.UsingStamina(StaminaCost)) { return true; }
        else {return false;}
    }

    public void CostDown(int cost)
    {
        StaminaCost = cost;
    }
}
