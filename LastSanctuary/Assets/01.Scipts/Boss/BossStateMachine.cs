using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public Boss Boss { get; private set; }
    public BossGroggyState GroggyState { get; private set; }

    public BossStateMachine(Boss boss)
    {
        this.Boss = boss;
        GroggyState = new BossGroggyState(this);
    }
}
