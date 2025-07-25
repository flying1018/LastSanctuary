using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : EnemyWeapon
{
    private Rigidbody2D _rigidbody2D;

    //생성
    public void Init(int damage, float knockback)
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Damage = damage;
        knockBackForce = knockback;
    }

    //발사
    public void Shot(Vector2 dir, float arrowPower)
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
            if (iguardable.ApplyGuard(Damage, Condition, transform, DamageType.Range))
            {
                ObjectPoolManager.Set((int)ObjectPoolManager.PoolingIndex.Arrow, gameObject, gameObject);
                return;
            }
        }

        //공격
        if (other.TryGetComponent(out IDamageable idamageable))
        {
            idamageable.TakeDamage(Damage, DamageType.Range, defpen);
        }

        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(this.transform, knockBackForce);
        }


        //충돌 시 파괴
        if(
            other.CompareTag(StringNameSpace.Tags.Wall) ||
            other.CompareTag(StringNameSpace.Tags.Ground) ||
            other.CompareTag(StringNameSpace.Tags.Celling) ||
            other.CompareTag(StringNameSpace.Tags.Player)
        )

        {
            ObjectPoolManager.Set((int)ObjectPoolManager.PoolingIndex.Arrow, gameObject, gameObject);
        }
    }
}