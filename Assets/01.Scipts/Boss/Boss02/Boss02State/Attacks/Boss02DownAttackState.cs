using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02DownAttackState : Boss02AttackState
{
    public Boss02DownAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

    public override void PhysicsUpdate()
    {
        Move(Vector2.down * _attackInfo.projectilePower);
    }
}
