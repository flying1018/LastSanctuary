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
        //넉백
        //사다리나 밧줄에서 피격시 추가 넉백
        _hitStart = Time.time;
        _condition.InvincibleStart = Time.time;
        _condition.IsInvincible = true;
        Debug.Log("경직 상태+무적 상태");
    }

    public override void Exit()
    {
        
    }

    public override void PhysicsUpdate()
    {
        if (Time.time - _hitStart >= _data.hitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}