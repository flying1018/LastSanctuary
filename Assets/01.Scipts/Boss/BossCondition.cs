using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCondition : Condition,IDamageable, IGroggyable
{
    //필드
    protected Boss _boss;
    protected int _maxGroggyGauge;
    protected float _groggyGauge;
    protected Coroutine _hitEffectCoroutine;
    
    //프로퍼티
    public bool IsGroggy {get; set;}
    public float GroggyGauge
    {
        get => _groggyGauge;
        set => _groggyGauge = Mathf.Clamp(value,0,_maxGroggyGauge);
    }
    public int MaxGroggyGauge => _maxGroggyGauge;

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
    

    
    //대미지 입을 때
    public void TakeDamage(WeaponInfo weaponInfo)
    {
        if (!IsAlive()) return;
        if (_isTakeDamageable) return;
        
        //대미지 계산
        ApplyDamage(weaponInfo.Attack,weaponInfo.Defpen);
        
        //죽었을 때
        if (!IsAlive())
        {   //죽음
            _boss.StateMachine.ChangeState(_boss.StateMachine.DeathState);
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
    public void ApplyGroggy(WeaponInfo weaponInfo)
    {
        if(_curHp <= 0) return;
        if (_isTakeDamageable) return;
        if (IsGroggy) return;
        //퍼가시 그로기 10증가
        //궁극기시 그로기 20증가
        
        //공격당 그로기 1/2/5씩증가
        _groggyGauge += weaponInfo.GroggyDamage;
        if (CheckGroggyState())
        {
            _boss.StateMachine.ChangeState(_boss.StateMachine.GroggyState);
        }
    }

    //그로기 상태로 가야 하는지 체크
    public bool CheckGroggyState()
    {
        return _groggyGauge >= _maxGroggyGauge;
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

    public float HpValue()
    {
        return _curHp/ _maxHp;
    }
    
    public float GroggyGaugeValue()
    {
        return (float)_groggyGauge / _maxGroggyGauge;
    }
}
