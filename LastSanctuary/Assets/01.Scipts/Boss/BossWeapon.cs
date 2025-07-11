using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeapon : Weapon
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(StringNameSpace.Tags.Enemy)) return;
        base.OnTriggerEnter2D(other);
    }
}
