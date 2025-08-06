using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02JugMirrorState : BossBaseState
{
    public Boss02JugMirrorState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        if (_boss02Event.TopMirror.position == _stateMachine2.TargetMirror)
        {
            _stateMachine2.ChangeState(_stateMachine2.DownAttack);
        }
    }
}
