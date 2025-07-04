

using UnityEngine;

public enum DamageType
{
    Attack,
    Contact,
    Heavy,
}

public interface IDamageable
{
 public void TakeDamage(int atk, DamageType type ,Transform attackDir , float knockBackPower);
 public void ApplyDamage(int totalDamage);
 public void KnockBack(Transform attackdir, float knockBackPower);
 public void ChangingHitState();
}
