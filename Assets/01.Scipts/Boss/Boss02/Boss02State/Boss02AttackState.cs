using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02AttackState : BossBaseState
{
    //공격 시간 체크
    protected BossAttackInfo _attackInfo;
    
    //공격 쿨타임 체크
    protected float _attackCoolTime;
    protected float _coolTime;

    
    public Boss02AttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine)
    {
        _attackInfo = attackInfo;
        
        _attackCoolTime = _attackInfo.coolTime;
        _coolTime = 0;
    }
    
    public override void Enter()
    {
        //쿨타임 초기화
        _attackCoolTime = _attackInfo.coolTime;
        _coolTime = 0;
        
        //애니메이션 실행
        _boss2.Animator.SetTrigger(_attackInfo.animParameter);
        _time = 0;
        
        //공격 정보 설정 후 전달
        _boss2.WeaponInfo.Attack = (int)(_data.attack * _attackInfo.multiplier);
        _boss2.WeaponInfo.KnockBackForce = _attackInfo.knockbackForce;
        _boss2.WeaponInfo.DamageType = DamageType.Heavy;
        
        _weapon.WeaponInfo = _boss2.WeaponInfo;

        //쿨타임 초기화
        _coolTime = 0;
    }
    
    public override void Update()
    {
        //공격이 끝나면 대기 상태
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime)
        {
            _stateMachine2.ChangeState(_stateMachine2.IdleState);
        }
    }
    
    public override void PhysicsUpdate()
    {
    }
    
    //쿨타임 체크
    public bool CheckCoolTime()
    {
        _coolTime += Time.deltaTime;
        return (_coolTime >= _attackCoolTime);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[0]);
    }
    
    public override void PlaySFX2()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[1]);
    }
}
