using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02ProjectileAttackState : Boss02AttackState
{
    public Boss02ProjectileAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(
        bossStateMachine, attackInfo)
    {
        _coolTime = _attackCoolTime;
    }
    
    public override void Enter()
    {
        //쿨타임 초기화
        _attackCoolTime = _attackInfo.coolTime;
        _coolTime = 0;
        
        //애니메이션 실행
        _boss2.Animator.SetBool(_attackInfo.animParameter,true);
        _time = 0;
        
        //공격 정보 설정 후 전달
        _boss2.WeaponInfo.Attack = (int)(_data.attack * _attackInfo.multiplier);
        _boss2.WeaponInfo.KnockBackForce = _attackInfo.knockbackForce;
        _boss2.WeaponInfo.DamageType = DamageType.Heavy;
        
        _weapon.WeaponInfo = _boss2.WeaponInfo;

        //쿨타임 초기화
        _coolTime = 0;
    }

    public override void Exit()
    {
        _boss2.Animator.SetBool(_attackInfo.animParameter,false);
    }

    public override void Update()
    {
        //공격이 끝나면 대기 상태
        _time += Time.deltaTime;
        if (_time > _attackInfo.AnimTime * 3)
        {
            _stateMachine2.ChangeState(_stateMachine2.IdleState);
        }
    }

    public override void PlayEvent1()
    {
        FireProjectile();
    }

    private void FireProjectile()
    {
        
        GameObject boomerang = ObjectPoolManager.Get(_attackInfo.projectilePrefab, (int)PoolingIndex.Boss02Projectile2);
        boomerang.transform.position = _boss02Event.GetRandomProjectilePosition();
        boomerang.transform.rotation = _weapon.transform.rotation;

        Vector2 targetDir = (_boss2.Target.position - boomerang.transform.position).normalized;
                            
        if (boomerang.TryGetComponent(out  ProjectileWeapon projectileWeapon))
        {
            projectileWeapon.Init((int)(_data.attack * _attackInfo.multiplier),_attackInfo.knockbackForce,PoolingIndex.Boss02Projectile2);
            projectileWeapon.Shot(targetDir,_attackInfo.projectilePower);
        }
    }
}
