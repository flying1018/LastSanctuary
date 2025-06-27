

using UnityEngine;

public enum DamageType
{
    Attack,
    Contact,
}

public interface IDamageable
{
 public void TakeDamage(int atk, DamageType type);
 
}
