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
    
    //투사체 날리기
    public void FireProjectile()
    {
        //투사체 생성 위치 설정
        float sizeX = _boss.SpriteRenderer.bounds.size.x /2;
        Transform firePoint = _boss.BossWeapon.transform;

        //투사체 생성
        GameObject attack2 = ObjectPoolManager.Get(_attackInfo.projectilePrefab, _attackInfo.projectilePoolId);
        
        //방향 설정
        Vector2 dir = _spriteRenderer.flipX ? Vector2.left : Vector2.right;
        attack2.transform.position = firePoint.position + (Vector3)(dir * sizeX);
        attack2.transform.right = dir;

        //
        if (attack2.TryGetComponent(out ArrowProjectile arrowPoProjectile))
        {
            arrowPoProjectile.Init(_data.attack, _attackInfo.knockbackForce);
            arrowPoProjectile.Shot(dir, _attackInfo.projectilePower);
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
