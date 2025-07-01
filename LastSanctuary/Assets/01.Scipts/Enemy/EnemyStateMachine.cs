using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }
    public float MoveSpeed { get; private set; }

    public GameObject Target { get; private set; }
    public EnemyIdleState IdleState { get; }
    public EnemyChaseState ChaseState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyReturnState ReturnState { get; }
    public EnemyHitState HitState { get; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;
        Target = GameObject.FindGameObjectWithTag("Player");

        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        ReturnState = new EnemyReturnState(this);
        HitState = new EnemyHitState(this);
    }
}
