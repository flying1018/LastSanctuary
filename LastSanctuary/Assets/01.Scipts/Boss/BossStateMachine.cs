using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public Boss Boss { get; private set; }
    public BossIdleState IdleState {get; private set;}
    public BossChaseState ChaseState { get; private set; }
    public BossGroggyState GroggyState { get; private set; }
    public Queue<BossAttackState> Attacks { get; private set; }
    public BossAttackState Attack1 { get; private set; }

    

    public BossStateMachine(Boss boss)
    {
        this.Boss = boss;
        IdleState = new BossIdleState(this);
        ChaseState = new BossChaseState(this); 
        GroggyState = new BossGroggyState(this);
        Attacks = new Queue<BossAttackState>();
        Attack1 = new BossAttackState(this, boss.Data.attacks[0]);
        
        ChangeState(IdleState);
    }
}
