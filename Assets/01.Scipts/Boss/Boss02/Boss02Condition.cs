using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Condition : BossCondition
{
    private Boss02 _boss02;
    
    public bool DontCollision { get; set; }
    
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
        if(_curHp <= 0) return;
        if (_isTakeDamageable) return;
        if (IsGroggy) return;
        
        //공격당 그로기 1/2/5씩증가
        _groggyGauge += weaponInfo.GroggyDamage;
        if (CheckGroggyState())
        {
            _boss02.StateMachine2.ChangeState(_boss02.StateMachine2.GroggyState);
        }

        if (_boss02.StateMachine2.currentState is Boss02RushAttackState)
        {
            DontCollision = true;
            _boss02.StateMachine2.ChangeState(_boss02.StateMachine2.AreaAttackIdle);       
        }
    }
    
    //대미지 입을 때
    public override void TakeDamage(WeaponInfo weaponInfo)
    {
        if (!IsAlive()) return;
        if (_isTakeDamageable) return;
        
        //대미지 계산
        ApplyDamage(weaponInfo.Attack,weaponInfo.Defpen);
        
        //죽었을 때
        if (!IsAlive())
        {   //죽음
            _boss02.StateMachine2.ChangeState(_boss02.StateMachine2.DeathState);
        }
        //그로기 상태가 아니고 페이즈 변환 조건이 되면
        else if(!IsGroggy && CheckPhaseShift())
        {   //페이즈 변환 상태로 전환
            _boss02.StateMachine2.ChangeState(_boss02.StateMachine2.PhaseShiftState);
        }
        else
        {   //피격 이펙트
            OnHitEffected();
        }
    }
}
