using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage { get; set;}
    public float KnockBackForce { get; set;}

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;

        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            idamageable.TakeDamage(Damage, DamageType.Attack,this.transform);
        }
        if (other.TryGetComponent(out IKnockBackable knockBackable))
        {
            knockBackable.ApplyKnockBack(transform, KnockBackForce);
        }
        
    }
}
