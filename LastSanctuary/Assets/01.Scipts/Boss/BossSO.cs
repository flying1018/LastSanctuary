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
    
    [Header("Movement")]
    public float moveSpeed = 3f;

    [Header("AttackState")] 
    public float attackIdleTime;
    public BossAttackInfo[] attacks;
    public float backJumpPower;
}
