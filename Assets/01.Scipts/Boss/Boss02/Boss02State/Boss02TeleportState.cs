using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02TeleportState : BossBaseState
{
    public Boss02TeleportState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }
    
    public override void Enter()
    {
        StartAnimation(_boss2.AnimationDB.WalkParameterHash);

        _time = 0;
        
        _boxCollider.enabled = false;
    }

    public override void Exit()
    {
        StopAnimation(_boss2.AnimationDB.WalkParameterHash);

        _boxCollider.enabled = true;
    }

    public override void Update()
    {
        _stateMachine2.AreaAttack.CheckCoolTime();
        
        _time += Time.deltaTime;
        if (_time > _data.TeleportTime)
        {
            _stateMachine2.ChangeState(_stateMachine2.JugMirror);
        }
    }

    public override void PlayEvent1()
    {
        _boss2.transform.position = _stateMachine2.TargetMirror;
    }
}
