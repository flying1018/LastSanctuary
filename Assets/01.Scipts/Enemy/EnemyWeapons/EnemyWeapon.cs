using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터의 무기 역할
/// </summary>
public class EnemyWeapon : Weapon
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;

        base.OnTriggerEnter2D(other);
    }
}
