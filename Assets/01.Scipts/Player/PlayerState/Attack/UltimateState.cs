using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : PlayerAttackState
{
    private int _hitCount;
    private float _interval;

    public UltimateState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
        _hitCount = _attackData.ultHitCount;
        _interval = _attackData.ultInterval;
        
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }


    public override void HandleInput()
    {

    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void PlayEvent1()
    {
        _player.StartCoroutine(UltimateSkill_Coroutine());
    }

    IEnumerator UltimateSkill_Coroutine()
    {
        //애니메이션 정지
        _player.Animator.speed = 0;
        
        //생성 및 위치 설정
        GameObject go = ObjectPoolManager.Get(_attackData.laserPrefab,(int)PoolingIndex.PlayerUlt);
        Vector3 pos = _player.transform.position;
        pos += 2 * (_spriteRenderer.flipX ? Vector3.left : Vector3.right);
        go.transform.position = pos;
        //방향 설정
        float dir = _spriteRenderer.flipX ? -180 : 0;
        go.transform.rotation = Quaternion.Euler(0, 0, dir);
        
        //필살기 데이터 설정
        go.TryGetComponent(out PlayerWeapon playerWeapon);
        playerWeapon.WeaponInfo = _player.WeaponInfo;
        playerWeapon.UltAttackInit(_hitCount, _interval,(int)PoolingIndex.PlayerUlt);
        
        //필살기 실행
        playerWeapon.UltAttack();
        
        //필살기 시간동안 정지
        yield return new WaitForSeconds(_hitCount * _interval);
        
        //실행
        _player.Animator.speed = 1;
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }


}
