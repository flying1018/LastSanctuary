using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 보스의 상태머신
/// </summary>
public class BossStateMachine : StateMachine
{
    public Boss Boss { get; protected set; }
    public BossIdleState IdleState {get; protected set;}
    public BossChaseState ChaseState { get; protected set; }
    public BossGroggyState GroggyState { get; protected set; }
    public BossSpawnState SpawnState { get; protected set; }
    public Queue<BossAttackState> Attacks { get; protected set; }
    public BossChopDownAttackState Attack1 { get; protected set; }
    public BossProjectileAttackState Attack2 { get; protected set; }
    public BossJumpAttackState Attack3 { get; protected set; }
    public BossPhaseShiftState PhaseShiftState { get; protected set; }
    public BossDeathState DeathState { get; protected set; }

    
    //부모 클래스 BossStateMachine
    public BossStateMachine(Boss boss)
    {
        this.Boss = boss;
        IdleState = new BossIdleState(this);
        ChaseState = new BossChaseState(this); 
        GroggyState = new BossGroggyState(this);
        SpawnState = new BossSpawnState(this); 
        Attacks = new Queue<BossAttackState>();
        Attack1 = new BossChopDownAttackState(this, boss.Data.attacks[0]);
        Attack2 = new BossProjectileAttackState(this, boss.Data.attacks[1]);
        Attack3 = new BossJumpAttackState(this, boss.Data.attacks[2]);
        PhaseShiftState = new BossPhaseShiftState(this);
        DeathState = new BossDeathState(this);
        
        ChangeState(SpawnState);
    }

    public BossStateMachine()
    {
        
    }
}
