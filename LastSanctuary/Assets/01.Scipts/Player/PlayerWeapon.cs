using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int Damage { get; set;}
    public float knockBackForce { get; set;}
    public int groggyDamage { get; set;}
    public float defpen { get; set;}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;
        
        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(this.transform, knockBackForce);
        }
        //그로기
        if (other.TryGetComponent(out IBossDamageable ibossdamageable))
        {
            ibossdamageable.ApplyGroggy(groggyDamage);
        }
        //공격
        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            idamageable.TakeDamage(Damage,DamageType.Attack,transform,defpen);
        }
    }
}
