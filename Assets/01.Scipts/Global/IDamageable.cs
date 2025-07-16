

using UnityEngine;


public enum DamageType
{
    Attack,
    Contact,
    Heavy,
}

/// <summary>
/// 대미지 가능한 인터페이스
/// </summary>
public interface IDamageable
{
 public void TakeDamage(int atk, DamageType type ,Transform attackDir, float defpen =0);
}

/// <summary>
/// 넉백 가능한 인터페이스
/// </summary>
public interface IKnockBackable 
{
    public void ApplyKnockBack(Transform attackDir, float knockBackPower);
}


/// <summary>
/// 그로기 가능한 인터페이스
/// </summary>
public interface IGroggyable 
{
   public void ApplyGroggy(int amount);
}
