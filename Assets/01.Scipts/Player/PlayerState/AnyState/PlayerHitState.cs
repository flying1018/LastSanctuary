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
        _player.Animator.SetTrigger(_player.AnimationDB.HitParameterHash);
        
        //피격 애니메이션
        _hitStart = Time.time;

        
        //공격 타입에 따른 경직 시간 설정
        switch (_condition.DamageType)
        {
            case DamageType.Range:
                _hitDuration = _data.lightHitDuration; //0.5f
                PlaySFX2();
                break;
            case DamageType.Heavy:
                _hitDuration = _data.heavyHitDuration; //1f
                PlaySFX2();
                break;
            case DamageType.Magic:
                _hitDuration = _data.lightHitDuration; //0.5f
                PlaySFX3();
                break;
            case DamageType.Trap:
                _hitDuration = _data.lightHitDuration; //0.5f
                PlaySFX4();
                break;
            default:
                _hitDuration = _data.lightHitDuration; //0.5f
                PlaySFX1();
                break;
        }
        
        //무적 시간 시작
        _condition.InvincibleFunc(_data.invincibleDuration);
        //히트 연출
        SoundManager.Instance.MuffleSound(false);
        SoundManager.Instance.MuffleSound(true,0.2f);
        UIManager.Instance.FadeOut(1,Color.black,_data.invincibleDuration *3);
    }

    public override void Exit()
    {
        SoundManager.Instance.MuffleSound(false,2f);
    }

    public override void HandleInput()
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
        //넉백이 끝나면
        if(_move.AddForceCoroutine != null) return;
        //중력 적용
        ApplyGravity();
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.hitSound);
    }

    public override void PlaySFX2()
    {
        SoundManager.Instance.PlaySFX(_data.arrowHitSound);
    }
    
    public override void PlaySFX3()
    {
        SoundManager.Instance.PlaySFX(_data.magicHitSound);
    }
    
    private void PlaySFX4()
    {
        SoundManager.Instance.PlaySFX(_data.trapHitSound);
    }
}