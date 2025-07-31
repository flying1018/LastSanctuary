using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkill", menuName = "Player/PlayerSkill")]
public class PlayerSkillSO : ScriptableObject
{
    [Header("StrongAttack")]
    public int downStaminaCost;

    [Header("Guard")] 
    public float reduceDamageRate;

    [Header("Excution")] 
    public float groggyTime;
}
