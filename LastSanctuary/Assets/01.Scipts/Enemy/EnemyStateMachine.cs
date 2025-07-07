using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }
    public EIdleState IdleState { get; private set; }
    public EAttackState AttackState { get; private set;}
    public EChaseState ChaseState { get; private set;}
    public EReturnState ReturnState { get; private set;}
    public EnemyHitState HitState { get; private set;}
    public EnemyDetectState DetectState { get; private set;}
    public EnemyBattleState BattleState { get; private set;}
    

    public float attackCoolTime;

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        switch (enemy.IdleType)
        {
            case IdleType.Idle:
                IdleState = new EnemyIdleState(this);
                break;
            case IdleType.Patrol:
                IdleState = new EnemyPatrolState(this);
                break;
        }
        ChaseState = new EnemyChaseState(this);
        switch (enemy.AttackType)
        {
            case AttackType.Melee:
                AttackState = new EnemyAttackState(this);
                break;
            case AttackType.Range:
                AttackState = new EnemyRangeAttackState(this);
                break;
        }
       
        ReturnState = new EnemyReturnState(this);
        switch (enemy.MoveType)
        {
            case MoveType.Walk:
                ChaseState = new EnemyChaseState(this);
                ReturnState = new EnemyReturnState(this);
                break;
            case MoveType.Fly:
                ChaseState = new EnemyFlyChaseState(this);
                ReturnState = new EnemyFlyReturnState(this);
                break;
        }
        AttackState = new EnemyAttackState(this);
        HitState = new EnemyHitState(this);
        DetectState = new EnemyDetectState(this);
        BattleState = new EnemyBattleState(this);
        
        ChangeState(IdleState);
    }
}
