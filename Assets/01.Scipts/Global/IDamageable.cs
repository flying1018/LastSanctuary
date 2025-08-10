

using UnityEngine;


public enum DamageType
{
    Attack,
    Range,
    Heavy,
    Magic,
}

/// <summary>
/// 대미지 가능한 인터페이스
/// </summary>
public interface IDamageable
{
 public void TakeDamage(WeaponInfo weaponInfo);
}

/// <summary>
/// 넉백 가능한 인터페이스
/// </summary>
public interface IKnockBackable 
{
    public void ApplyKnockBack(WeaponInfo weaponInfo, Transform attackDir);
}


/// <summary>
/// 그로기 가능한 인터페이스
/// </summary>
public interface IGroggyable 
{
   public void ApplyGroggy(WeaponInfo weaponInfo);
}

public interface IGuardable
{
    public bool ApplyGuard(WeaponInfo weaponInfo,Transform dir);
}
