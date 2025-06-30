using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int Damage { get; set;}

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;
            Debug.Log(idamageable);
            idamageable.TakeDamage(Damage, DamageType.Contact);
        }
    }
    
}
