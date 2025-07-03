using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int Damage { get; set;}

    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;
            Debug.Log(idamageable + "는 " + Damage +"의 를 받았다.");
            idamageable.TakeDamage(Damage,transform, DamageType.Attack);
        }
    }
}
