using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : BossAttackState
{
    public ProjectileAttack(BossStateMachine bossStateMachine, BossAttackInfo attackInfo) : base(bossStateMachine, attackInfo) { }

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
        float duration = 0.6f;
        float timer = 0f;

        Vector2 startPos = _rigidbody.position;
        Vector2 backDir = (startPos - (Vector2)_boss.Target.position);
        Vector2 horizontalDir = new Vector2(backDir.normalized.x, 0f);
        
        while (timer < duration)
        {
            float t = timer / duration;
            float x = _data.backjumpDistance * t;
            float y = 4f * _data.backjumpHeight * t * (1f - t);
            
            Vector2 newPos = startPos + horizontalDir * x + Vector2.up * y;
            _rigidbody.MovePosition(newPos);

            timer += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
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
}
