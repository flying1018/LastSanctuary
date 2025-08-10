using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : EnemyWeapon
{
    protected Rigidbody2D _rigidbody2D;
    protected PoolingIndex _poolingIndex;

    //생성
    public void Init(WeaponInfo weaponInfo,PoolingIndex poolingIndex)
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        WeaponInfo.Attack = weaponInfo.Attack;
        WeaponInfo.KnockBackForce = weaponInfo.KnockBackForce;
        WeaponInfo.DamageType = weaponInfo.DamageType;
        _poolingIndex = poolingIndex;
    }

    //발사
    public virtual void Shot(Vector2 dir, float arrowPower)
    {
        _rigidbody2D.velocity = dir * arrowPower;
    }

    //충돌 시
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;

        //가드
        if (other.TryGetComponent(out IGuardable iguardable))
        {
            if (iguardable.ApplyGuard(WeaponInfo, transform))
            {
                ObjectPoolManager.Set(gameObject, (int)_poolingIndex);
                return;
            }
        }

        //공격
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            idamageable.TakeDamage(WeaponInfo);
        }

        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(WeaponInfo,transform);
        }


        //충돌 시 파괴
        if(other.CompareTag(StringNameSpace.Tags.Wall) ||
           other.CompareTag(StringNameSpace.Tags.Ground) ||
           other.CompareTag(StringNameSpace.Tags.Celling) ||
           other.CompareTag(StringNameSpace.Tags.Player))
        {
            ObjectPoolManager.Set(gameObject, (int)_poolingIndex);
        }
    }
}