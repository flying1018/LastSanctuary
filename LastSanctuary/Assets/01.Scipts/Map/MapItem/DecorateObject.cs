using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorateObject : MonoBehaviour, IDamageable
{
    public bool isBroken;
    public int durability;

    public void TakeDamage(int atk, DamageType type, Transform attackDir, float knockBackPower)
    {
        if (isBroken) { return; }

        durability--;
        // 피격시 부셔지는 애니메이션 있다면 추가

        if (durability <= 0)
        {
            isBroken = true;
        }
        // BreakObj(this.gameobject)
    }

    public void ApplyDamage(int totalDamage)
    {

    }

    public void KnockBack(Transform attackdir, float knockBackPower)
    {
        return;
    }

    public void ChangingHitState()
    {
        return;
    }
}
