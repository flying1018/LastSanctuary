using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; private set; }
    public EIdleState IdleState { get; private set; }
    public EChaseState ChaseState { get; private set;}
    public EAttackState AttackState { get; private set;}
    public EReturnState ReturnState { get; private set;}
    public EnemyHitState HitState { get; private set;}
    public EnemyDetectState DetectState { get; private set;}
    public EnemyBattleState BattleState { get; private set;}
    public EnemyDeathState DeathState { get; private set;}
    public EnemyGroggyState GroggyState { get; private set;}
    

    public float attackCoolTime;

    public EnemyStateMachine(Enemy enemy)
    {
        this.Enemy = enemy;

        //타입에 따라 생성되는 상태 머신 변경
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
        ReturnState = new EnemyReturnState(this);
        switch (enemy.AttackType)
        {
            case AttackType.Melee:
                AttackState = new EnemyAttackState(this);
                break;
            case AttackType.Range:
                AttackState = new EnemyRangeAttackState(this);
                break;
            case AttackType.Rush:
                AttackState = new EnemyRushAttack(this);
                break;
        }
        HitState = new EnemyHitState(this);
        DetectState = new EnemyDetectState(this);
        BattleState = new EnemyBattleState(this);
        DeathState = new EnemyDeathState(this);
        GroggyState = new EnemyGroggyState(this);
        
        ChangeState(IdleState);
    }
}
