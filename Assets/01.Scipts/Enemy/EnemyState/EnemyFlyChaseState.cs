using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyChaseState : EChaseState
{
    public EnemyFlyChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Exit()
    {
        base.Exit();
        Fly(Vector2.zero);
    }

    protected override void Chase()
    {
        if(_enemy.Target == null) return;
        Vector2 direction = DirectionToTarget();
        Fly(direction);
        Rotate(direction);
    }
}
