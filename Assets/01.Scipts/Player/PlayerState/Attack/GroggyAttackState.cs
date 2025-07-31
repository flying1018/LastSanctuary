using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroggyAttackState : PlayerAttackState
{
    private float[] _angles;
    private WaitForSeconds _waitIntervalSec;

    public GroggyAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
        _angles = new float[] { 0f, 30f, 150f, 0f, 180f, -30f, -150f };
        _waitIntervalSec = new WaitForSeconds(_attackData.groggyAttackInterval);
    }
    
    public override void Enter()
    {
        base.Enter();

        Vector3 dir = DirectionToTarget();
        Rotate(dir);
        
        _condition.IsInvincible = true;
    }

    public override void Exit()
    {
        base.Exit();
        
        _condition.IsInvincible = false;
    }
    
    public override void HandleInput()
    {

    }
    
    public override void Update()
    {

    }

    public override void PlayEvent1()
    {
        base.PlayEvent1();

        _player.StartCoroutine(GroggyAttack_Coroutine());
    }

    IEnumerator GroggyAttack_Coroutine()
    {
        //애니메이션 정지
        _player.Animator.speed = 0;
        
        yield return new WaitForSeconds(0.5f);
        //투명화
        _player.SpriteRenderer.material = _data.transparentMaterial;
        _player.SpriteRenderer.color = new Color(1, 1, 1, 0);
        
        //공격력 설정
        _player.WeaponInfo.Attack = (int)(_condition.TotalAttack * AttackInfo.multiplier);
        _player.WeaponInfo.KnockBackForce = AttackInfo.knockbackForce;
        _player.WeaponInfo.GroggyDamage = AttackInfo.groggyDamage;
        
        //연출 및 공격
        foreach (float angle in _angles )
        {
            GroggyAttackEffect(angle);
            yield return _waitIntervalSec;
        }
        
        //모습 정상화
        _player.SpriteRenderer.material = _data.defaultMaterial;
        _player.SpriteRenderer.color = Color.white;
        
        //애니 정상화
        _player.Animator.speed = 1;

        //적은 즉사 처리
        if (_player.Target.TryGetComponent(out Enemy enemy))
        {
            TargetIsEnemy(enemy);
        }
        //보스는 이동만
        else if (_player.Target.TryGetComponent(out Boss boss))
        {
            TargetIsBoss(boss);
        }
        
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    private void GroggyAttackEffect(float angle)
    {
        GameObject go = ObjectPoolManager.Get(_attackData.groggyAttackPrefab,(int)PoolingIndex.PlayerGroggy);
        if (go.TryGetComponent(out PlayerWeapon weapon))
        {
            //그로기 애니메이션에 필요한 정보
            weapon.GroggyAttackInit(_attackData.groggyAnimInterval, (int)PoolingIndex.PlayerGroggy);
            
            //무기에 대미지 전달
            weapon.WeaponInfo = _player.WeaponInfo;
            
            //애니메이션 실행
            weapon.GroggyAttack();
        }
        go.transform.position = _player.Target.transform.position;
        go.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    private void TargetIsEnemy(Enemy enemy)
    {
        //적 뒤로 순간이동
        Vector3 dir = DirectionToTarget();
        dir.x += (dir.x > 0) ? enemy.CapsuleCollider.size.x + 2: -enemy.CapsuleCollider.size.x -2;
        Vector3 goal = new Vector3(_player.Target.transform.position.x + dir.x,_player.transform.position.y, 0);
        _player.transform.position = goal;

        //적은 즉사 처리
        enemy.Condition.Death();
    }
    
    private void TargetIsBoss(Boss boss)
    {
        //보스 뒤로 순간이동
        Vector3 dir = DirectionToTarget();
        dir.x += (dir.x > 0) ? boss.BoxCollider.size.x + 2: -boss.BoxCollider.size.x -2;
        Vector3 goal = new Vector3(_player.Target.transform.position.x + dir.x, _player.transform.position.y, 0);
        _player.transform.position = goal;
        
    }

    //적의 방향
    private Vector3 DirectionToTarget()
    {
        Vector3 dir = _player.Target.transform.position - _player.transform.position;

        dir.y = 0;
        dir.z = 0;
        return dir.normalized;
    }
}
