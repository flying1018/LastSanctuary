using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttackState : EAttackState
{
    public EnemyRangeAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine) {}

    public void FireArrow()
    {
        Transform firePoint = _enemy.EnemyWeapon.transform;

        GameObject arrow = ObjectPoolManager.Get(_data.arrowPrefab, _data.arrowPoolId);
        Debug.Log(arrow);
        arrow.transform.position = firePoint.position;

        Vector2 dir = DirectionToTarget();
        arrow.transform.right = dir;

        if (arrow.TryGetComponent(out ArrowProjectile arrowPoProjectile))
        {
            arrowPoProjectile.Init(_data.arrowPrefab);
            arrowPoProjectile.Shot(dir, _data.arrowPower);
        }
    }
}
