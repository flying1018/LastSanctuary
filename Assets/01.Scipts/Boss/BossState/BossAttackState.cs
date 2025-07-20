using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackState : BossBaseState
{
    //공격 시간 체크
    protected BossAttackInfo _attackInfo;
    
    //공격 쿨타임 체크
    protected float _attackCoolTime;
    protected float _coolTime;
    
    public BossAttackState(BossStateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine)
    {
        _attackInfo = attackInfo;
        
        _attackCoolTime = _attackInfo.coolTime;
        _coolTime = 0;
    }

    public override void Enter()
    {
        //방향 설정
        Vector2 dir = DirectionToTarget();
        Rotate(dir);
        
        //쿨타임 초기화
        _attackCoolTime = _attackInfo.coolTime;
        _coolTime = 0;
        
        //애니메이션 실행
        _boss.Animator.SetTrigger(_attackInfo.animParameter);
        _time = 0;
        
        //공격
        _weapon.Condition = _condition;
        _weapon.Damage = (int)(_data.attack * _attackInfo.multiplier);
        _weapon.defpen = _data.defpen;
        _weapon.knockBackForce = _attackInfo.knockbackForce;
    }

    public override void Update()
    {
        //공격이 끝나면 대기 상태
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
    }

    //쿨타임 체크
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

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[0]);
    }
    
    public override void PlaySFX2()
    {
        SoundManager.Instance.PlaySFX(_attackInfo.attackSounds[1]);
    }
}
