using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossStateMachine : StateMachine
{
    public BossIdleState IdleState {get; private set;}
    public BossChaseState ChaseState { get; private set; }
    
  
    public Boss Boss { get; private set; }
    
    public BossStateMachine(Boss boss)
    {
        Boss = boss;
        
        ChaseState = new BossChaseState(this); //상태초기화
        IdleState = new BossIdleState(this);
    }
}
