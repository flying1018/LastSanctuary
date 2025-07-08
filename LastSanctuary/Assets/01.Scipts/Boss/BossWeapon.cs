using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int Damage { get; set;}
    public float KnockBackForce { get; set;}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;
            idamageable.TakeDamage(Damage, DamageType.Attack,this.transform,KnockBackForce);
        }
    }
}
