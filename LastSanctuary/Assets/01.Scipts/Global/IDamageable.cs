

using UnityEngine;

public enum DamageType
{
    Attack,
    Contact,
    Heavy,
}

public interface IDamageable
{
 public void TakeDamage(int atk,Transform attackDir = null ,DamageType type = DamageType.Attack);
 public void ApplyDamage(int totalDamage);
 public void KnockBack(Transform attackdir);
 public void ChangingHitState();
}
