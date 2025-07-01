

using UnityEngine;

public enum DamageType
{
    Attack,
    Contact,
    Heavy,
}

public interface IDamageable
{
 public void TakeDamage(int atk,Transform attackDir  ,DamageType type );
 
}
