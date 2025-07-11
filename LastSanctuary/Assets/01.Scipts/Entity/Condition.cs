using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
