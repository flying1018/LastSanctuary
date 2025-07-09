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
    [Header("Condition")] 
    public int attack;
    public int defense;
    public int hp;
    public int groggyGauge;
    public float groggyDuration;
    public float damageDelay;
    
    [Header("Movement")]
    public float moveSpeed = 3f;
    
    [Header("Attack")]
    public float attackRange = 50f;
    
    [field: Header("Anime Time")]
    [field: SerializeField] public float spawnAnimeTime { get; private set; } = 10f;

    [Header("AttackState")] 
    public float attackIdleTime;
    public BossAttackInfo[] attacks;
    public float backJumpPower;
    public float defpen;
}
