using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.TryGetComponent(out IDamageable idamageable)) { return; }

        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player))
        {
            idamageable.TakeDamage(damage, transform, DamageType.Contact);
        }
    }
}
