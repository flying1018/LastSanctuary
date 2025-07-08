using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroggyState : BossBaseState
{
    public BossGroggyState(BossStateMachine bossStateMachine) : base(bossStateMachine)
    {
    }

    private float _groggyStart;
    public override void Enter()
    {
        _groggyStart = Time.time;
    }

    public override void Exit()
    {
    }

    public override void PhysicsUpdate()
    {
        if (Time.time - _groggyStart >= _data.groggyDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}
