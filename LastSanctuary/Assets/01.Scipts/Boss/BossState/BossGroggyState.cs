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
        _condition.IsGroggy = true;
        Debug.Log("GroggyState Enter");
    }

    public override void Exit()
    {
        _condition.IsGroggy = false;
        Debug.Log("GroggyState Exit");
    }

    public override void PhysicsUpdate()
    {

        if (Time.time - _groggyStart < _data.groggyDuration)
            return;
            
        _condition.ChangePhase2State();
        _stateMachine.ChangeState(_stateMachine.IdleState);
        
        
    }
}
