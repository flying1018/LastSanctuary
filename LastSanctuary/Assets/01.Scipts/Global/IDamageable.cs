

using UnityEngine;

public enum DamageType
{
    Attack,
    Contact,
}

public interface IDamageable
{
 public void TakeDamage(float amount, GameObject attacker, DamageType type);
 public void ApplyDamage(float damage);
 public void Die();
}
