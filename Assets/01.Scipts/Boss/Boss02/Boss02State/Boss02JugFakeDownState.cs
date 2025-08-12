using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02JugFakeDownState : BossBaseState
{
    public Boss02JugFakeDownState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        _boss2.transform.position = _boss02Event.GetRandomTopPosition();

        int judgment = Random.Range(0, 2);
        if (judgment == 0)
        {
            _stateMachine2.ChangeState(_stateMachine2.DownAttack);
        }
        else
        {
            _stateMachine2.ChangeState(_stateMachine2.FakeAttack);
        }
            
        
    }
}
