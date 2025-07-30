using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Skill
{
    None,
    DashAttack,
    AirDash,
    DashLengthUp,
    DashInvincible,
    StrAttackStmDown,
    StrAttackDamUp,
    StrAttackDer,
    Execution,
    GuardDamDecUp,
    PerGuardJugUp,
    ReversalGuard,
    ExecutionTimeUp,
    ExecutionHpRec,
    SerialExecutions,
    UltimateStmUp,
    UltimateTimeUp,
    UltimateRangeUp,
    UltimateHpRec,
    UltimateTimeStop,
}

[System.Serializable]
public class SkillInfo
{
    public Skill skill;
    public bool open;
}

public class PlayerSkill : MonoBehaviour
{
    private Player _player;
    private SkillInfo[] _skills;
    private PlayerSkillSO _skillData;
    
    public float DamageReduceRate { get; private set; }
    public float GroggyTime { get; private set; }
    
    public void Init(Player player)
    {
        _player = player;
        _skillData = _player.SkillData;
        
        _skills = new SkillInfo[Enum.GetValues(typeof(Skill)).Length];
        foreach (Skill skill in Enum.GetValues(typeof(Skill)))
        {
            _skills[ (int)skill ] = new SkillInfo();
            _skills[ (int)skill ].skill = skill;
            _skills[ (int)skill ].open = false;
        }

        DamageReduceRate = _player.Data.damageReduction;
        GroggyTime = _player.AttackData.groggyTime;
    }

    public SkillInfo GetSkill(Skill skill)
    {
        return _skills[(int)skill];
    }

    public void SetSkillData(Skill skill)
    {
        switch (skill)
        {
            case Skill.StrAttackStmDown:
                _player.StateMachine.StrongAttack.CostDown(_skillData.downStaminaCost);
                break;
            case Skill.GuardDamDecUp:
                DamageReduceRate = _skillData.reduceDamageRate;
                break;
            case Skill.ExecutionTimeUp:
                GroggyTime = _skillData.groggyTime;
                break;
        }
    }

}
