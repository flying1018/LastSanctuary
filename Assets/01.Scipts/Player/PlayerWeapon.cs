using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 무기
public class PlayerWeapon : Weapon
{
    //필드 그로기
    private Coroutine _groggyAttackCoroutine;
    private WaitForSeconds _waitGroggyAnimSec;
    private int _objectPoolId;
    
    //필드 필살기
    private Coroutine _ultAttackCoroutine;
    private WaitForSeconds _waitUltInitAnimSec;
    private WaitForSeconds _waitUltPartAnimSec;
    private WaitForSeconds _waitUltExtSec;
    private bool _isUltimate;
    private int _hitCount;
    private float _hitInterval;
    private Vector2 _boxCenter;

    //직렬화
    [SerializeField] private SpriteRenderer spriteRenderer1;
    [SerializeField] private SpriteRenderer spriteRenderer2;
    [SerializeField] private Sprite[] sprites1;
    [SerializeField] private Sprite[] sprites2;

    [Header("Ultimate")] 
    [SerializeField] private GameObject[] particle1;
    [SerializeField] private GameObject[] particle2;
    [SerializeField] private float initAnimInterval;
    [SerializeField] private float extAnimInterval;
    [SerializeField] private float particleInterval;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask playerLayer;
    

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
        _waitGroggyAnimSec = new WaitForSeconds(animInterval);
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
            yield return _waitGroggyAnimSec;
        }

        _groggyAttackCoroutine = null;
        ObjectPoolManager.Set(gameObject, _objectPoolId);
    }
    
    public void UltAttackInit(int hitCount ,float hitInterval)
    {
        _hitCount = hitCount;
        _hitInterval = hitInterval;
        
        //생성 애니메이션 간격
        _waitUltInitAnimSec = new WaitForSeconds(initAnimInterval);
        
        //파티클 생성 간격
        float middleTime = _hitCount * _hitInterval - (extAnimInterval * sprites1.Length) - (initAnimInterval * sprites1.Length);
        int particleNum = particle1.Length + particle2.Length;
        _waitUltPartAnimSec = new WaitForSeconds(middleTime/particleNum);
        
        //소멸 애니메이션 간격
        _waitUltExtSec = new WaitForSeconds(extAnimInterval);
        
        // 이 위치 기준으로 판정할 Box 중심
        float boxCenterX = transform.rotation.z == 0 ?  boxSize.x / 2 : -boxSize.x / 2;;
        _boxCenter = new Vector2(transform.position.x + boxCenterX, transform.position.y);
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
        _isUltimate = true;
        spriteRenderer1.sprite = sprites1[sprites1.Length-1];
        spriteRenderer2.sprite = sprites2[sprites2.Length-1];
        
        //생기는 애니메이션
        for(int i=sprites1.Length-1; i>=0; i--)
        {
            spriteRenderer1.sprite = sprites1[i];
            spriteRenderer2.sprite = sprites2[i];
            yield return _waitUltInitAnimSec;
        }
        
        //파티클 애니메이션
        int j = 0;
        Vector3 dir = (transform.rotation.z == 0 ? Vector3.right : Vector3.left) * particleInterval; 
        
        for (int i=0;i < particle1.Length;i++)
        {
           GameObject go = ObjectPoolManager.Get(particle1[i],(int)PoolingIndex.UltParticle1);
           go.transform.position = transform.position + dir * (j + 1);
           go.TryGetComponent(out ParticleAnim particleAnim);
           particleAnim.Init(PoolingIndex.UltParticle1);
           j++;
           yield return _waitUltPartAnimSec;
        }
        for (int i=0;i < particle2.Length;i++)
        {
            GameObject go = ObjectPoolManager.Get(particle2[i],(int)PoolingIndex.UltParticle2);
            go.transform.position = transform.position + dir * (j + 1);
            go.TryGetComponent(out ParticleAnim particleAnim);
            particleAnim.Init(PoolingIndex.UltParticle2);
            j++;
            yield return _waitUltPartAnimSec;
        }
        
        
        //사라지는 애니메이션
        for(int i=0; i<sprites1.Length; i++)
        {
            spriteRenderer1.sprite = sprites1[i];
            spriteRenderer2.sprite = sprites2[i];
            yield return _waitUltExtSec;
        }

        //사라지기
        _isUltimate = false;
        _ultAttackCoroutine = null;
        ObjectPoolManager.Set(gameObject,(int)PoolingIndex.PlayerUlt);
    }
    
    
    private void FixedUpdate()
    {
        if (!_isUltimate) return;

        // 박스 충돌 검사
        Collider2D[] hits = Physics2D.OverlapBoxAll(_boxCenter, boxSize, 0f, playerLayer);

        foreach (var hit in hits)
        {
            //그로기
            if (hit.TryGetComponent(out IGroggyable ibossdamageable))
            {
                ibossdamageable.ApplyGroggy(WeaponInfo);
            }
            //공격
            if (hit.TryGetComponent(out IDamageable idamageable) )
            {
                idamageable.TakeDamage(WeaponInfo);
            }
            //넉백
            if (hit.TryGetComponent(out IKnockBackable iknockBackable))
            {
                iknockBackable.ApplyKnockBack(WeaponInfo, transform);
            }
            if (hit.TryGetComponent(out Condition condition))
            {
                condition.DamageDelay(_hitInterval);
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 boxCenter = new Vector2(transform.position.x + boxSize.x / 2, transform.position.y);
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
