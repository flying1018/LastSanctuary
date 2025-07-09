using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCondition : MonoBehaviour, IBossDamageable
{
    //필드
    private Boss _boss;
    private int _attack;
    private int _defence;
    private int _maxHp;
    private int _curHp;
    private int _maxGroggyGauge;
    private int _groggyGauge;
    private bool _isTakeDamageable;
    private int _damage;
    
    //프로퍼티
    public bool IsGroggy {get; set;}

    public void Init(Boss boss)
    {
        _boss = boss;
        _attack = boss.Data.attack;
        _defence = boss.Data.defense;
        _maxHp = boss.Data.hp;
        _maxGroggyGauge = boss.Data.groggyGauge;
        
        _curHp = _maxHp;
        _groggyGauge = 0;
        
        _isTakeDamageable = false;
    }
    private IEnumerator DamageDelay_Coroutine()
    {
        _isTakeDamageable = true;
        yield return new WaitForSeconds(_boss.Data.damageDelay);
        _isTakeDamageable = false;
    }
    
    
    public void TakeDamage(int atk, DamageType type, Transform attackDir, float defpen)
    {
        if (_isTakeDamageable) return;
        Debug.Log(_curHp);
        ApplyDamage(atk);
    }
    
    //보스 그로기 증가
    public void ApplyGroggy(int groggyDamage)
    {
        if (_isTakeDamageable) return;
        //퍼가시 그로기 10증가
        //궁극기시 그로기 20증가
        
        //공격당 그로기 1/2/5씩증가
        _groggyGauge += groggyDamage;
        Debug.Log(_groggyGauge);
        if (_groggyGauge >= _maxGroggyGauge)
        {
            _groggyGauge = 0;
            ChangingState();
        }
        StartCoroutine(DamageDelay_Coroutine());
    }
    
    public void ApplyDamage(int atk)
    {
        if (IsGroggy)
        {
            _damage = (int)Math.Ceiling(atk * 1.5f); //계산식
        }
        else
        {
            _damage = atk;
        }
        _curHp = _damage;
        Debug.Log(_damage);
    }
    
    public void ChangingState()
    {
        //궁극기 맞을시 1초간 전환
    Debug.Log(_boss.StateMachine);
       _boss.StateMachine.ChangeState(_boss.StateMachine.GroggyState);
    }
}
