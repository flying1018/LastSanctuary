using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스 공격의 역할을 하는 무기
/// </summary>
public class BossWeapon : Weapon
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;
        //공격
        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            idamageable.TakeDamage(Damage,DamageType.Heavy,transform,defpen);
        }
        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(this.transform, knockBackForce);
        }
    }
}
