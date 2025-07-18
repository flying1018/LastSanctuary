using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitState : PlayerBaseState
{
    public PlayerHitState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    private float _hitStart;
    private float _hitDuration;
    public override void Enter()
    {
        //피격 애니메이션
        _hitStart = Time.time;

        _condition.InvincibleFunc(_hitDuration);
        
        //공격 타입에 따른 경직 시간 설정
        switch (_condition.DamageType)
        {
            case DamageType.Heavy:
                _hitDuration = _data.HeavyHitDuration; //0.5f
                break;
            default:
                _hitDuration = _data.LightHitDuration; //0.2f
                break;
        }
        
        PlaySFX1();
    }

    public override void Exit()
    {
        
    }

    public override void Update()
    {
        //경직 시간이 끝나면
        if (Time.time - _hitStart >= _hitDuration)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.hitSound);
    }
}