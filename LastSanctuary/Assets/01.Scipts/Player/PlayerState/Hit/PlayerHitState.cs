using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    private float _hitStart;
    public override void Enter()
    {
        //피격 애니메이션
        _hitStart = Time.time;
        _condition._invincibleStart = Time.time;
        _condition.IsInvincible = true;
        Debug.Log("경직 상태+무적 상태");
    }

    public override void Exit()
    {
        Debug.Log("경직 해제");
    }

    public override void PhysicsUpdate()
    {
        if (Time.time - _hitStart >= _data.hitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}