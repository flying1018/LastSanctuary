using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour , IDamageable
{
    public void TakeDamage(int atk, Transform attackDir, DamageType type)
    {
        Debug.Log("TakeDamage");
    }

    public void ApplyDamage(int totalDamage)
    {
        throw new System.NotImplementedException();
    }

    public void KnockBack(Transform attackdir)
    {
        throw new System.NotImplementedException();
    }

    public void ChangingHitState()
    {
        throw new System.NotImplementedException();
    }
}
