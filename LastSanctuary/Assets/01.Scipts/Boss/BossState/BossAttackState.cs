using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    //공격 시간 체크
    private BossAttackInfo _attackInfo;
    private float _time;
    
    //공격 쿨타임 체크
    private float _attackCoolTime;
    private float _coolTime;
    
    
    public BossAttackState(BossStateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine)
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
        _boss.Animator.SetTrigger(_attackInfo.animParameter);
        _time = 0;
        
        //공격
        _weapon.Damage = (int)(_data.attack * _attackInfo.multiplier);
        _weapon.defpen = _data.defpen;
        _weapon.KnockBackForce = _attackInfo.knockbackForce;
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public bool CheckCoolTime()
    {
        _coolTime += Time.deltaTime;
        if (_coolTime > _attackCoolTime)
        {
            _coolTime = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}
