using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02AreaAttackIdleState : Boss02IdleState
{

    public Boss02AreaAttackIdleState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }
    
    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _data.attackIdleTime)
        {
            //시간 초기화
            _time = 0;

            if (_stateMachine2.AreaAttack.CheckCoolTime())
            {
                _stateMachine2.ChangeState(_stateMachine2.AreaAttack);
            }
            else
            {
                _stateMachine2.TargetMirror = _boss02Event.GetRandomMirror();
                _stateMachine2.ChangeState(_stateMachine2.TeleportState);
            }
            
        }
    }

}
