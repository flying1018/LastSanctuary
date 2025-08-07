using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02StateMachine : StateMachine
{
    public Boss02 Boss { get; private set; }
    public Boss02IdleState IdleState {get; private set;}
    public Boss02AreaAttackIdleState AreaAttackIdle { get; private set; }
    public Boss02TeleportState TeleportState { get; private set; }
    public Boss02GroggyState GroggyState { get; private set; }
    public Boss02SpawnState SpawnState { get; private set; }
    public Boss02PhaseShiftState PhaseShiftState { get; private set; }
    public Boss02DeathState DeathState { get; private set; }
    public Boss02JugMirrorState JugMirror { get; private set; }
    public Boss02DownAttackState DownAttack { get; private set; }
    public Boss02AreaAttackState AreaAttack { get; private set; }
    public Boss02RushAttackState RushAttack { get; private set; }
    public Boss02BoomerangAttackState BoomerangAttack { get; private set; }
    
    public Queue<BossAttackState> Attacks { get; private set; }
    
    public Vector3 TargetMirror { get; set;}

    public Boss02StateMachine(Boss02 boss)
    {
        Boss = boss;
        IdleState = new Boss02IdleState(this);
        AreaAttackIdle = new Boss02AreaAttackIdleState(this);
        SpawnState = new Boss02SpawnState(this);
        TeleportState = new Boss02TeleportState(this);
        JugMirror = new Boss02JugMirrorState(this);
        GroggyState = new Boss02GroggyState(this);
        PhaseShiftState = new Boss02PhaseShiftState(this);
        DeathState = new Boss02DeathState(this);
        
        DownAttack = new Boss02DownAttackState(this, boss.Data.attacks[0]);
        AreaAttack = new Boss02AreaAttackState(this, boss.Data.attacks[1]);
        RushAttack = new Boss02RushAttackState(this, boss.Data.attacks[2]);
        BoomerangAttack = new Boss02BoomerangAttackState(this, boss.Data.attacks[3]);
        
        ChangeState(SpawnState);
    }
}
