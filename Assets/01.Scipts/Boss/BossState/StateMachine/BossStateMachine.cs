using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 보스의 상태머신
/// </summary>
public class BossStateMachine : StateMachine
{
    public Boss Boss { get; private set; }
    public BossIdleState IdleState {get; private set;}
    public BossChaseState ChaseState { get; private set; }
    public BossGroggyState GroggyState { get; private set; }
    public BossSpawnState SpawnState { get; private set; }
    public Queue<BossAttackState> Attacks { get; private set; }
    public BossChopDownAttackState Attack1 { get; private set; }
    public BossProjectileAttackState Attack2 { get; private set; }
    public BossJumpAttackState Attack3 { get; private set; }
    public BossPhaseShiftState PhaseShiftState { get; private set; }
    public BossDeathState DeathState { get; private set; }

    
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
}
