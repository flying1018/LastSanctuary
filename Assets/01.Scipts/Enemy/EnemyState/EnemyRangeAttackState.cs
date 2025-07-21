using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EAttackState
{
    public EnemyRangeAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine) {}

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Vector2 direction = DirectionToTarget();
        Rotate(direction);
    }

    public override void PlayEvent1()
    {
        FireArrow();
    }

    //원거리 공격
    public void FireArrow()
    {
        Transform firePoint = _enemy.EnemyWeapon.transform;

        //오브젝트 생성
        GameObject arrow = ObjectPoolManager.Get(_data.arrowPrefab, _data.arrowPoolId);
        arrow.transform.position = firePoint.position;

        //쏘는 방향 설정
        Vector2 dir = DirectionToTarget();
        arrow.transform.right = dir;

        //투사체 발사
        if (arrow.TryGetComponent(out ArrowProjectile arrowProjectile))
        {
            arrowProjectile.Init(_data.attack, _data.knockbackForce);
            arrowProjectile.Shot(dir, _data.arrowPower);
        }
        
        
        //사운드
        //_enemy.PlaySFX1();
    }
}
