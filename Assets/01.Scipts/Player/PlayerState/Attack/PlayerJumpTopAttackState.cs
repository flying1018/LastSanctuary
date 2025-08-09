using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpTopAttackState : JumpAttackState
{
    public PlayerJumpTopAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }
    
    public override void Enter()
    {
        base.Enter();
    }

 
}
