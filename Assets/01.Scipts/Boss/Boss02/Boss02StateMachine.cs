using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02StateMachine : StateMachine
{
    public Boss02 Boss { get; protected set; }
    public BossIdleState IdleState {get; protected set;}
    public Boss02TeleportState TeleportState { get; protected set; }
    public BossGroggyState GroggyState { get; protected set; }
    public Boss02SpawnState SpawnState { get; protected set; }
    public BossPhaseShiftState PhaseShiftState { get; protected set; }
    public BossDeathState DeathState { get; protected set; }
    
    public Queue<BossAttackState> Attacks { get; protected set; }
    
    public Vector2 TargetMirror { get; set;}

    public Boss02StateMachine(Boss02 boss)
    {
        Boss = boss;
        SpawnState = new Boss02SpawnState(this);
        
    }
}
