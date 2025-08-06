using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02JugMirrorState : BossBaseState
{
    public Boss02JugMirrorState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        base.Enter();
        
        if (_stateMachine2.TargetMirror == _boss02Event.TopMirror.position )
        {
            _stateMachine2.ChangeState(_stateMachine2.DownAttack);
        }
        for (int i = 0; i < _boss02Event.LeftMirror.Length; i++)
        {
            if (_stateMachine2.TargetMirror == _boss02Event.LeftMirror[i].position)
            {
                _stateMachine2.TargetMirror = _boss02Event.RightMirror[2-i].position;
                _stateMachine2.ChangeState(_stateMachine2.RushAttack);
                return;
            }
        }
        for (int i = 0; i < _boss02Event.RightMirror.Length; i++)
        {
            if (_stateMachine2.TargetMirror == _boss02Event.RightMirror[i].position)
            {
                _stateMachine2.TargetMirror = _boss02Event.LeftMirror[2-i].position;
                _stateMachine2.ChangeState(_stateMachine2.RushAttack);
                return;
            }
        }

    }
}
