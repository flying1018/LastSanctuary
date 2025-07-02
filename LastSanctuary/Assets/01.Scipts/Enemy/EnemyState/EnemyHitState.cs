using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyBaseState
{
    public EnemyHitState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.HitParameterHash);
    }
}
