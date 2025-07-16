using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 보스의 공격 데이터
/// </summary>
[System.Serializable]
public class BossAttackInfo
{
    public float multiplier;
    public float knockbackForce;
    public float coolTime;
    public string animParameter;
    public AnimationClip attackAnim;
    public float AnimTime => attackAnim.length;
    //투사체 있는 경우
    public GameObject projectilePrefab;
    public int projectilePoolId;
    public int projectilePower;
    //사운드
    public AudioClip[] attackSounds;
}

/// <summary>
/// 보스의 초기 데이터
/// </summary>
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
    public float alphaValue;
    
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
    public float defpen;
    public float backjumpDistance = 3f;
    public float backjumpHeight = 2f;

    [Header("PhaseShiftState")] 
    public float phaseShiftHpRatio;
    public AnimationClip phaseShiftAnim;
    public float PhaseShiftTime => phaseShiftAnim.length;
    
    [Header("Sound")] 
    public AudioClip breathSound;
    public AudioClip howlingSound;
    public AudioClip jumpSound;
    public AudioClip landingSound;
    public AudioClip deathSound;
    public AudioClip phaseShiftSound;
    public AudioClip walkSound;
    
    [Header("Materials")]
    public Material[] materials;
}
