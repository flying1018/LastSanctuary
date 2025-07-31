using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 무기
public class PlayerWeapon : Weapon
{
    //필드 그로기
    private Coroutine _groggyAttackCoroutine;
    private WaitForSeconds _waitAnimSec;
    private int _objectPoolId;
    
    //필드 필살기
    private Coroutine _ultAttackCoroutine;
    private int _hitCount;
    private float _hitInterval;
    private bool _isUltimate;
    
    //직렬화
    [SerializeField] private SpriteRenderer spriteRenderer1;
    [SerializeField] private SpriteRenderer spriteRenderer2;
    [SerializeField] private Sprite[] sprites1;
    [SerializeField] private Sprite[] sprites2;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;

        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out Condition condition))
        {
            condition.DamageDelay();
        }

        if (other.CompareTag(StringNameSpace.Tags.Enemy))
        {
            if (WeaponInfo.Condition is PlayerCondition dummy)
            {
                dummy.CurUltimate += WeaponInfo.UltimateValue;
            }
        }
    }
    public void GroggyAttackInit(float animInterval, int id)
    {
        _waitAnimSec = new WaitForSeconds(animInterval);
        _objectPoolId = id;
    }

    public void GroggyAttack()
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
            yield return _waitAnimSec;
        }

        _groggyAttackCoroutine = null;
        ObjectPoolManager.Set(gameObject, _objectPoolId);
    }
    
    public void UltAttackInit(int hitCount, float hitInterval,int id)
    {
        _hitCount = hitCount;
        _hitInterval = hitInterval;
        _objectPoolId = id;
        _waitAnimSec = new WaitForSeconds(hitInterval);
    }

    public void UltAttack()
    {
        if (_ultAttackCoroutine != null)
        {
            StopCoroutine(_ultAttackCoroutine);
            _ultAttackCoroutine = null;
        }
        StartCoroutine(UltAttackAnim_Coroutine());
    }

    IEnumerator UltAttackAnim_Coroutine()
    {
        //초기설정 스프라이트
        spriteRenderer1.sprite = sprites1[0];
        spriteRenderer2.sprite = sprites2[0];
        
        //공격 가능 처리
        _isUltimate = true;
        
        yield return new WaitForSeconds(_hitCount*_hitInterval - _hitInterval*sprites1.Length);
        
        //사라지는 애니메이션
        for(int i=0; i<sprites1.Length; i++)
        {
            spriteRenderer1.sprite = sprites1[i];
            spriteRenderer2.sprite = sprites2[i];
            yield return _waitAnimSec;
        }

        //사라지기
        _ultAttackCoroutine = null;
        ObjectPoolManager.Set(gameObject, _objectPoolId);
    }
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;
        if(!_isUltimate) return;

        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out Condition condition))
        {
            condition.DamageDelay();
            
            //필살기 딜레이
            _isUltimate = false;
            Invoke(nameof(DemageDelay),_hitInterval);
        }
    }

    private void DemageDelay()
    {
        _isUltimate = true;
    }
}
