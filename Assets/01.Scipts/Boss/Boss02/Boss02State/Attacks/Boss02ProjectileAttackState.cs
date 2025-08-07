using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02ProjectileAttackState : Boss02AttackState
{
    public Boss02ProjectileAttackState(Boss02StateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }
    
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
