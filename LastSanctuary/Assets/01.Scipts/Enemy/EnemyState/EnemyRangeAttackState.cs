using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EnemyAttackState
{
    public EnemyRangeAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine) {}

    public void FireArrow()
    {
        Transform firePoint = _enemy.EnemyWeapon.transform;

        GameObject arrow = ObjectPoolManager.Get(_data.arrowPrefab, _data.arrowPoolId);
        arrow.transform.position = firePoint.position;
        
        Vector2 dir = (_enemy.Target.position - firePoint.position).normalized;
        arrow.transform.right = dir;

        if (arrow.TryGetComponent(out ArrowProjectile arrowPoProjectile))
        {
            arrowPoProjectile.Init(dir, _data.attack, _data.arrowSpeed, _data.knockbackForce);
        }
    }
}
