using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossAttackInfo
{
    public float multiplier;
    public float knockbackForce;
    public float coolTime;
    public string animParameter;
    public AnimationClip attackAnim;
    public float AnimTime => attackAnim.length;
}

[CreateAssetMenu(fileName = "Boss", menuName = "new Boss")]
public class BossSO : ScriptableObject
{
    [Header("Info")]
    public int _key;
    public string _name;
    
    [Header("Condition")] 
    public int attack;
    public int defense;
    public int hp;
    public int groggyGauge;
    public float groggyDuration;
    public float damageDelay;
    
    public AnimationClip deathAnim;
    public float deathEventDuration; 
    public float deathTime => deathAnim.length + deathEventDuration;
    
    [Header("ChaseState")]
    public float moveSpeed = 2f;
    
    [Header("SpawnState")]
    public AnimationClip spawnAnim;
    public float SpawnAnimeTime {get => spawnAnim.length;}

    [Header("AttackState")] 
    public float attackRange = 2f;
    public float attackIdleTime;
    public BossAttackInfo[] attacks;
    public float backJumpPower;
    public float defpen;

    [Header("PhaseShiftState")] 
    public float phaseShiftHpRatio;
    public AnimationClip phaseShiftAnim;
    public float PhaseShiftTime => phaseShiftAnim.length;
}
