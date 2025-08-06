using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Condition : BossCondition
{
    private Boss02 _boss02;
    
    public bool PlayerPerfectGuard { get; set; }
    
    public void Init(Boss02 boss)
    {
        _boss = boss;
        _boss02 = boss;
        
        _maxHp = boss.Data.hp;
        _curHp = _maxHp;
        _attack = boss.Data.attack;
        _defence = boss.Data.defense;
        _maxGroggyGauge = boss.Data.groggyGauge;
        _groggyGauge = 0;
        _delay = boss.Data.damageDelay;
        _isTakeDamageable = false;
    }

    public override void ApplyGroggy(WeaponInfo weaponInfo)
    {
        base.ApplyGroggy(weaponInfo);

        if (_boss02.StateMachine2.currentState is Boss02RushAttackState)
        {
            PlayerPerfectGuard = true;
            _boss02.StateMachine2.ChangeState(_boss02.StateMachine2.AreaAttackIdle);       
        }
    }
}
