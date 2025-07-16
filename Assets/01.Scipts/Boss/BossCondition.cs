using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCondition : Condition,IDamageable, IGroggyable
{
    //필드
    private Boss _boss;
    private int _maxGroggyGauge;
    private int _groggyGauge;
    private Coroutine _hitEffectCoroutine;
    
    //프로퍼티
    public bool IsGroggy {get; set;}

    public void Init(Boss boss)
    {
        _boss = boss;
        
        _maxHp = boss.Data.hp;
        _curHp = _maxHp;
        _attack = boss.Data.attack;
        _defence = boss.Data.defense;
        _maxGroggyGauge = boss.Data.groggyGauge;
        _groggyGauge = 0;
        _delay = boss.Data.damageDelay;
        _isTakeDamageable = false;
    }
    
    //죽었을 때
    public void Death()
    {
        StartCoroutine(Death_Coroutine());
    }
    private IEnumerator Death_Coroutine()
    {
        //죽을 때 필요한 소리 설정
        if (_boss.StateMachine.currentState is BossBaseState bossBaseState)
        {
            bossBaseState.SoundClip[0] = _boss.Data.deathSound;
        }
        //애니메이션 실행
        _boss.Animator.SetTrigger(_boss.AnimationDB.DeathParameterHash);
        //키네마틱 켜기
        _boss.KinematicOn();
        //보스 사망 연출
        _boss.BossEvent.OnTriggerBossDeath();
        //죽는 시간 만큼 대기
        yield return new WaitForSeconds(_boss.Data.deathTime);
        //제거
        ObjectPoolManager.Set(_boss.Data._key, _boss.gameObject, _boss.gameObject);
    }
    
    //대미지 입을 때
    public void TakeDamage(int atk, DamageType type, Transform attackDir, float defpen)
    {
        if (!IsAlive()) return;
        if (_isTakeDamageable) return;
        
        //대미지 계산
        ApplyDamage(atk,defpen);
        
        //죽었을 때
        if (!IsAlive())
        {   //죽음
            Death(); 
        }
        //그로기 상태가 아니고 페이즈 변환 조건이 되면
        else if(!IsGroggy && CheckPhaseShift())
        {   //페이즈 변환 상태로 전환
            _boss.StateMachine.ChangeState(_boss.StateMachine.PhaseShiftState);
        }
        else
        {   //피격 이펙트
            OnHitEffected();
        }
    }
    
    //대미지 계산
    public void ApplyDamage(int atk ,float defpen)
    {
        int damage;
        if (IsGroggy)
        {
            damage = (int)(Math.Ceiling((atk - _defence * (1 - defpen)) * 1.5f));
        }
        else
        {
            damage = (int)(Math.Ceiling(atk - _defence * (1 - defpen)));
        }
        _curHp -= damage;
    }
    
    //피격 이펙트
    private void OnHitEffected()
    {
        if (_hitEffectCoroutine != null)
        {
            StopCoroutine(_hitEffectCoroutine);
            _hitEffectCoroutine = null;       
        }
        _hitEffectCoroutine = StartCoroutine(HitEffect_Coroutine());
    }

    private IEnumerator HitEffect_Coroutine()
    {
        SpriteRenderer sprite = _boss.SpriteRenderer;
        
        sprite.material = _boss.Data.materials[3];
        yield return new WaitForSeconds(_boss.Data.damageDelay);
        sprite.material = _boss.Phase2 ? _boss.Data.materials[1] : _boss.Data.materials[0];
    }
    
    //보스 그로기 증가
    public void ApplyGroggy(int groggyDamage)
    {
        if(_curHp <= 0) return;
        if (_isTakeDamageable) return;
        if (IsGroggy) return;
        //퍼가시 그로기 10증가
        //궁극기시 그로기 20증가
        
        //공격당 그로기 1/2/5씩증가
        _groggyGauge += groggyDamage;
        if (CheckGroggyState())
        {
            _boss.StateMachine.ChangeState(_boss.StateMachine.GroggyState);
        }
    }

    //그로기 상태로 가야 하는지 체크
    public bool CheckGroggyState()
    {
        if (_groggyGauge >= _maxGroggyGauge)
        {
            _groggyGauge = 0;
            return true;
        }

        return false;
    }

    //페이즈 변환 상태로 가야 하는지 체크
    public bool CheckPhaseShift()
    {
        if (!IsAlive()) return false; 
        
        float phase2Hp = _maxHp * _boss.Data.phaseShiftHpRatio;
        return (!_boss.Phase2 && _curHp <= phase2Hp);
    }

    //살아 있는지 체크
    public bool IsAlive()
    {
        return _curHp > 0;
    }
}
