using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02BoomerangAttackState : Boss02AttackState
{
    Vector2 _dir;
    float _margin = 1f;
    public Boss02BoomerangAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

    public override void Enter()
    {
        base.Enter();
        
        _dir = (_stateMachine2.MoveTarget - _boss2.transform.position).normalized;
        Rotate(_dir);
    }


    public override void PlayEvent1()
    {
        ThrowBoomerang();
    }

    private void ThrowBoomerang()
    {
        GameObject boomerang = ObjectPoolManager.Get(_attackInfo.projectilePrefab, (int)PoolingIndex.Boss02Projectile1);
        boomerang.transform.position = new Vector2(_weapon.transform.position.x,_weapon.transform.position.y - _margin);
        boomerang.transform.rotation = _weapon.transform.rotation;

        if (boomerang.TryGetComponent(out BoomerangProjectile boomerangProjectile))
        {
            boomerangProjectile.Init(_boss2.WeaponInfo,PoolingIndex.Boss02Projectile1);
            boomerangProjectile.Shot(_dir,_attackInfo.projectilePower);
        }
    }
}
