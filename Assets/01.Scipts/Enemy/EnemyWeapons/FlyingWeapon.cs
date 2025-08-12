using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingWeapon : EnemyWeapon
{
    private EnemyFlyingAttack _flyingAttack;

    public void Init(EnemyFlyingAttack flyingAttack)
    {
        _flyingAttack = flyingAttack;
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Celling))
        {
            _flyingAttack.ChangeIdleState();
        }
    }
}
