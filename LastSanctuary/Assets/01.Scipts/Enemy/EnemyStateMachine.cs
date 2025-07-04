using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }
    public EnemyIdleState IdleState { get; private set; }
    public EnemyChaseState ChaseState { get; private set;}
    public EnemyAttackState AttackState { get; private set;}
    public EnemyReturnState ReturnState { get; private set;}
    public EnemyHitState HitState { get; private set;}
    public EnemyDetectState DetectState { get; private set;}
    public EnemyPatrolState PatrolState { get; private set; }

    public float attackCoolTime;

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        IdleState = new EnemyIdleState(this);
        ChaseState = new EnemyChaseState(this);
        AttackState = new EnemyAttackState(this);
        ReturnState = new EnemyReturnState(this);
        HitState = new EnemyHitState(this);
        DetectState = new EnemyDetectState(this);
        PatrolState = new EnemyPatrolState(this);
        
         switch (enemy.Type)
        {
            case MonsterType.Idle:
                ChangeState(IdleState);
                break;
            case MonsterType.Patrol:
                ChangeState(PatrolState);
                break;
        }
    }
}
