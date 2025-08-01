using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//매개변수가 늘어날 때 마다 수정할 게 너무 많아서 구조체 생성
public struct WeaponInfo
{
    public Condition Condition; //컨디션
    public int Attack;          //공격력
    public float KnockBackForce;//넉백량
    public int GroggyDamage;    //그로기 대미지
    public float Defpen;        //방어 무시율
    public DamageType DamageType;//대미지 타입
    public float UltimateValue; //궁극기 게이지 증가량
}


/// <summary>
/// 무기의 부모
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    public WeaponInfo WeaponInfo;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        //가드
        if (other.TryGetComponent(out IGuardable iguardable))
        {
            if (iguardable.ApplyGuard(WeaponInfo,transform))
                return;
        }
        //그로기
        if (other.TryGetComponent(out IGroggyable ibossdamageable))
        {
            ibossdamageable.ApplyGroggy(WeaponInfo);
        }
        //공격
        if (other.TryGetComponent(out IDamageable idamageable) )
        {
            idamageable.TakeDamage(WeaponInfo);
        }
        //넉백
        if (other.TryGetComponent(out IKnockBackable iknockBackable))
        {
            iknockBackable.ApplyKnockBack(WeaponInfo, transform);
        }
    }
}
