using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroggyAttack : PlayerWeapon
{
    private Coroutine _groggyAttackCoroutine;
    private WaitForSeconds _waitGroggyAnimSec;
    private int _objectPoolId;
    
    [SerializeField] private Sprite[] sprites1;
    [SerializeField] private SpriteRenderer spriteRenderer1;
    
    public void Init(float animInterval, int id)
    {
        _waitGroggyAnimSec = new WaitForSeconds(animInterval);
        _objectPoolId = id;
    }

    public void GroggyAtk()
    {
        if (_groggyAttackCoroutine != null)
        {
            StopCoroutine(_groggyAttackCoroutine);
            _groggyAttackCoroutine = null;
        }

        _groggyAttackCoroutine = StartCoroutine(GroggyAttack_Coroutine());
    }

    IEnumerator GroggyAttack_Coroutine()
    {
        for (int i = 0; i < sprites1.Length; i++)
        {
            spriteRenderer1.sprite = sprites1[i];
            yield return _waitGroggyAnimSec;
        }

        _groggyAttackCoroutine = null;
        ObjectPoolManager.Set(gameObject, _objectPoolId);
    }
}
