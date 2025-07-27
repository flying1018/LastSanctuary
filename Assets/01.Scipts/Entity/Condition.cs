using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어, 몬스터, 보스 공통으로 사용되는 컨디션의 부모
/// </summary>
public class Condition : MonoBehaviour
{
    protected float _maxHp;
    protected float _curHp;
    protected int _attack;
    protected int _defence;
    protected float _delay;
    protected bool _isTakeDamageable;
    protected Coroutine _damageDelayCoroutine;
    
    public void DamageDelay()
    {
        //오브젝트가 꺼졌을 때 실행하지 않음.
        if(!gameObject.activeInHierarchy) return;
        
        if (_damageDelayCoroutine != null)
        {
            StopCoroutine(_damageDelayCoroutine);
            _damageDelayCoroutine = null;       
        }
        _damageDelayCoroutine = StartCoroutine(DamageDelay_Coroutine());
    }
    
    protected IEnumerator DamageDelay_Coroutine()
    {
        _isTakeDamageable = true;
        yield return new WaitForSeconds(_delay);
        _isTakeDamageable = false;

        _damageDelayCoroutine = null;
    }


}
