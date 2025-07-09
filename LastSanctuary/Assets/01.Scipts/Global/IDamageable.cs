

using UnityEngine;


public enum DamageType
{
    Attack,
    Contact,
    Heavy,
}

public interface IDamageable
{
 public void TakeDamage(int atk, DamageType type ,Transform attackDir, float defpen =0);
}

public interface IKnockBackable : IDamageable
{
    public void ApplyKnockBack(Transform attackDir, float knockBackPower);
}

public interface IBossDamageable : IDamageable
{
   public void ApplyGroggy(int amount);
}
