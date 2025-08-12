using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EAttackState
{
    public EnemyRangeAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine) {}
    
    public override void Enter()
    {
        //공격에 관련된 정보 초기화
        _time = 0;
        _stateMachine.attackCoolTime = 0;

        //공격 중간은 Idle 애니메이션
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.AttackParameterHash);

        //공격력 정보 넘겨주기
        _enemy.EnemyWeapon.WeaponInfo = _enemy.WeaponInfo;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 direction = DirectionToTarget();
        Rotate(direction);
    }

    public override void PlayEvent1()
    {
        FireProjetile();
    }

    //원거리 공격
    public void FireProjetile()
    {
        Transform firePoint = _enemy.EnemyWeapon.transform;
        
        //오브젝트 생성
        GameObject projectileOb = ObjectPoolManager.Get(_data.projectilePrefab, (int)_data.poolIndex);
        projectileOb.transform.position = firePoint.position;

        //쏘는 방향 설정
        Vector2 dir = DirectionToTarget();
        if (_enemy.Data.rangeType == RangeType.Beeline)
        {
            dir = new Vector2(dir.x, 0).normalized;
        }
        projectileOb.transform.right = dir;

        //투사체 발사
        if (projectileOb.TryGetComponent(out ProjectileWeapon projectile))
        {
            projectile.Init(_enemy.WeaponInfo, _data.poolIndex);
            projectile.Shot(dir, _data.projectilePower);
        }
        
        
        //사운드
        PlaySFX1();
    }
}
