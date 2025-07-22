using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : EnemyWeapon
{
    private Rigidbody2D _rigidbody2D;
    private KinematicMove _kinematicMove;

    //생성
    public void Init(int damage, float knockback)
    {
        _kinematicMove = GetComponent<KinematicMove>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        float sizeX = collider.size.x * transform.localScale.x;
        float sizeY = collider.size.y * transform.localScale.y;
        _kinematicMove.Init(sizeX, sizeY, _rigidbody2D);

        DebugHelper.Log($"{sizeX} {sizeY}");
        Damage = damage;
        knockBackForce = knockback;
    }

    //발사
    public void Shot(Vector2 dir, float arrowPower)
    {
        _kinematicMove.gravityScale = Vector2.zero;
        _kinematicMove.AddForce(dir * arrowPower, 1f);
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