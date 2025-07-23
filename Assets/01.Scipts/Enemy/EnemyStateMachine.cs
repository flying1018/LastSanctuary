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
    public EDetectState DetectState { get; private set;}
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
        switch (enemy.AttackType)
        {
            case AttackType.Melee:
                DetectState = new EnemyDetectState(this);
                AttackState = new EnemyAttackState(this);
                break;
            case AttackType.Range:
                DetectState = new EnemyDetectState(this);
                AttackState = new EnemyRangeAttackState(this);
                break;
            case AttackType.Flying:
                DetectState = new EnemyFlyingDetectState(this);
                AttackState = new EnemyFlyingAttack(this);
                break;
        }
        ChaseState = new EnemyChaseState(this);
        ReturnState = new EnemyReturnState(this);
        HitState = new EnemyHitState(this);
        BattleState = new EnemyBattleState(this);
        DeathState = new EnemyDeathState(this);
        GroggyState = new EnemyGroggyState(this);
        
        ChangeState(IdleState);
    }
}
