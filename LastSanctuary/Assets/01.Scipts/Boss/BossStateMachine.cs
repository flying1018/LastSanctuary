using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public Boss Boss { get; private set; }
    public BossIdleState IdleState { get; private set; }
    public BossGroggyState GroggyState { get; private set; }

    public BossStateMachine(Boss boss)
    {
        this.Boss = boss;
        IdleState = new BossIdleState(this);
        GroggyState = new BossGroggyState(this);
        
        ChangeState(IdleState);
    }
}
