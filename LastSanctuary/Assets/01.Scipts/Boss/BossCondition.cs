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
    
    public override void Death()
    {
        StartCoroutine(Death_Coroutine());
    }
    private IEnumerator Death_Coroutine()
    {
        _boss.Animator.SetTrigger(_boss.AnimationDB.DeathParameterHash);
        _boss.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        yield return new WaitForSeconds(_boss.Data.deathTime);
        ObjectPoolManager.Set(_boss.Data._key, _boss.gameObject, _boss.gameObject);
    }
    
    public void TakeDamage(int atk, DamageType type, Transform attackDir, float defpen)
    {
        if (_isTakeDamageable) return;
        ApplyDamage(atk,defpen);
        if (_curHp <= 0)
        {
            Death();
        }
    }
    
    //보스 그로기 증가
    public void ApplyGroggy(int groggyDamage)
    {
        if (_isTakeDamageable) return;
        if (IsGroggy) return;
        //퍼가시 그로기 10증가
        //궁극기시 그로기 20증가
        
        //공격당 그로기 1/2/5씩증가
        _groggyGauge += groggyDamage;
        ChangeGroggyState();
    }

    public void ChangeGroggyState()
    {
        if (_groggyGauge >= _maxGroggyGauge)
        {
            _groggyGauge = 0;
            _boss.StateMachine.ChangeState(_boss.StateMachine.GroggyState);
        }
    }
    
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
        ChangePhase2State();
    }

    public void ChangePhase2State()
    {
        float phase2Hp = _maxHp * _boss.Data.phaseShiftHpRatio;
        if (!_boss.Phase2 && _curHp <= phase2Hp)
        {
            _boss.StateMachine.ChangeState(_boss.StateMachine.PhaseShiftState);
        }
    }
    
}
