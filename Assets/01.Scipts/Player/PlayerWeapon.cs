using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 무기
public class PlayerWeapon : Weapon
{
    //필드
    private Coroutine _groggyAttackCoroutine;
    private WaitForSeconds _waitAnimSec;
    private int _objectPoolId;
    
    
    //직렬화
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    public void GroggyAttackInit(float animInterval, int id)
    {
        _waitAnimSec = new WaitForSeconds(animInterval);
        _objectPoolId = id;
    }
    
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;
        
        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out Condition condition))
        {
            condition.DamageDelay();
        }
        
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
        spriteRenderer.sprite = sprites[0];
        yield return _waitAnimSec;
        spriteRenderer.sprite = sprites[1];
        yield return _waitAnimSec;
        spriteRenderer.sprite = sprites[2];
        yield return _waitAnimSec;

        _groggyAttackCoroutine = null;
        ObjectPoolManager.Set(_objectPoolId, gameObject, gameObject);
    }
}
