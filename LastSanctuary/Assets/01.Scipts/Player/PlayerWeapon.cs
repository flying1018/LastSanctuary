using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Player)) return;
        
        base.OnTriggerEnter2D(other);

        if (other.TryGetComponent(out Condition condition))
        {
            condition.DamageDelay();
        }
        
    }
}
