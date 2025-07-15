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
        //나갈 때 정지
        base.Exit();
        Fly(Vector2.zero);
    }

    protected override void Chase()
    {
        if(_enemy.Target == null) return;
        
        //플레이어를 향해 이동
        Vector2 direction = DirectionToTarget();
        Fly(direction);
        Rotate(direction);
    }
}
