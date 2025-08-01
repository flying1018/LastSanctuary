using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfo
{
    public AnimationClip attackAnim;
    public float animTime => attackAnim.length;
    public int attackIndex;     //공격 번호
    public float multiplier;    //공격 배수
    public float nextComboTime; //다음 콤보까지 여유 시간
    public int staminaCost;     //스테미나 코스트
    public float attackForce;   //공격시 전진 파워
    public float knockbackForce;//넉백 파워
    public int groggyDamage; //그로기 증가량
    public bool isInvincible; //해당 공격 시 무적여부
    public float ultimateValue;
}

[Serializable]
[CreateAssetMenu(fileName = "PlayerAttack", menuName = "Player/New PlayerAttack")]
public class PlayerAttackSO : ScriptableObject
{
    [Header("AttackState")] 
    public AttackInfo[] attacks;
    public float defpen;
    public float maxUltimateGauge;

    [Header("StrongAttack")] 
    public AttackInfo strongAttack;
    
    [Header("DashAttackState")] 
    public AttackInfo dashAttack;
    
    [Header("JumpAttackState")] 
    public AttackInfo jumpAttack;

    [Header("TopAttackState")] 
    public AttackInfo topAttack;

    [Header("GroggyAttackState")] 
    public GameObject groggyAttackPrefab;
    public float groggyAnimInterval;
    public AttackInfo groggyAttack;
    public float groggyTime;
    public float detectionRange;
    public float groggyAttackInterval;

    [Header("UltimateAttackState")]
    public AttackInfo ultAttack;
    public int ultHitCount;
    public float ultInterval;
    public GameObject laserPrefab;

}
