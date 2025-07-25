using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAttackState : BossAttackState
{
    public BossProjectileAttackState(BossStateMachine bossStateMachine, BossAttackInfo attackInfo) : base(
        bossStateMachine, attackInfo) { }

    public override void PlayEvent1()
    {
        BackJump();
    }
    
    public override void PlayEvent2()
    {
        FireProjectile();
    }
    
    //Attack2에서 사용하는 애니메이션 이벤트
    public void BackJump()
    {   
        _boss.StartCoroutine(BackJumpCoroutine());
    }

    private IEnumerator BackJumpCoroutine()
    {
        _time = 0;
        
        //플레이어의 반대 방향
        Vector2 targetDir = _boss.Target.position - _boss.transform.position;
        float x = targetDir.x > 0 ? -_data.backjumpDistance: _data.backjumpDistance;
        Vector2 jumpDir = new Vector2(x, _data.backjumpHeight);
        
        //점프 시간 계산 포물선을 그리기 위해선 2번 반복 4로 하면 원을 그림.
        float duration = 2/_data.backjumpSpeed;
        
        while (_time < duration)
        {
            _time += Time.deltaTime;

            jumpDir.y = _data.backjumpHeight -  _data.backjumpHeight * _time * _data.backjumpSpeed;
            
            _move.Move(jumpDir * _data.backjumpSpeed);
            yield return _move.WaitFixedUpdate;
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
        if (attack2.TryGetComponent(out ProjectileWeapon arrowPoProjectile))
        {
            arrowPoProjectile.Init(_data.attack, _attackInfo.knockbackForce);
            arrowPoProjectile.Shot(dir, _attackInfo.projectilePower);
        }
    }
}
