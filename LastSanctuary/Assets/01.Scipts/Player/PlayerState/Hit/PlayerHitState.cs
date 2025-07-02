using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine stateMachine) : base(stateMachine) {}
    private float _hitStart;
    private float _hitDuration;
    public override void Enter()
    {
        //피격 애니메이션
        //사다리나 밧줄에서 피격시 추가 넉백
        _hitStart = Time.deltaTime;
        _condition.InvincibleStart = Time.deltaTime;
        _condition.IsInvincible = true;
        switch (_condition.DamageType)
        {
            case DamageType.Heavy:
                _hitDuration = _data.HeavyHitDuration; //0.5f
                break;
            Default:
                _hitDuration = _data.LightHitDuration; //0.2f
                break;
        }
        Debug.Log("경직 상태+무적 상태");
    }

    public override void Exit()
    {
        
    }

    public override void PhysicsUpdate()
    {
        if (Time.deltaTime - _hitStart >= _hitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }
}