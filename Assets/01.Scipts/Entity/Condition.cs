using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 플레이어, 몬스터, 보스 공통으로 사용되는 컨디션의 부모
/// </summary>
public abstract class Condition : MonoBehaviour
{
    protected int _maxHp;
    protected int _curHp;
    protected int _attack;
    protected int _defence;
    protected float _delay;
    protected bool _isTakeDamageable;
    
    public void DamageDelay()
    {
        StartCoroutine(DamageDelay_Coroutine());
    }
    
    protected IEnumerator DamageDelay_Coroutine()
    {
        _isTakeDamageable = true;
        yield return new WaitForSeconds(_delay);
        _isTakeDamageable = false;
    }

    public abstract void Death();





}
