using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기의 부모
/// </summary>
public class Weapon : MonoBehaviour
{
    public Condition Condition { get; set; }
    public int Damage { get; set;}
    public float knockBackForce { get; set;}
    public int groggyDamage { get; set;}
    public float defpen { get; set;}

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //가드
        if (other.TryGetComponent(out IGuardable iguardable))
        {
            if (iguardable.ApplyGuard(Damage,Condition,transform,DamageType.Attack))
                return;
        }
        //그로기
        if (other.TryGetComponent(out IGroggyable ibossdamageable))
        {
            ibossdamageable.ApplyGroggy(groggyDamage);
        }
        //공격
        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            idamageable.TakeDamage(Damage,DamageType.Attack ,defpen);
        }
        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(this.transform, knockBackForce);
        }
    }
}
