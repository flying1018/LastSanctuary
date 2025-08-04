using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateState : PlayerAttackState
{
    private int _hitCount;
    private float _interval;
    private bool _ultimateStart;

    public UltimateState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo)
    {
        _hitCount = _attackData.ultHitCount;
        _interval = _attackData.ultInterval;
        
    }

    public override void Enter()
    {
        base.Enter();

        _ultimateStart = false;
        
        if(_skill.GetSkill(Skill.UltimateStmUp).open)
            _condition.CurStamina = _condition.MaxStamina;
        
        //UI 꺼짐 연출
        UIManager.Instance.OnOffUI(false);
        
        //줌인 연출
        _camera.StartZoomCamera(_player.transform,3f);
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
        //게이지 감소
        if(!_ultimateStart) return;
        _condition.CurUltimate -= (_condition.MaxUltimate/(_hitCount*_interval)) * Time.deltaTime;
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
        //UI 켜짐
        UIManager.Instance.OnOffUI(true);
        //줌인 연출 종료
        _camera.EndZoomCamera();
        _camera.ShakeCamera(2,2,_hitCount*_interval);
        
        //애니메이션 정지
        _player.Animator.speed = 0;
        
        //게이지 감소 시작
        _ultimateStart = true;
        
        //생성 및 위치 설정
        GameObject go = ObjectPoolManager.Get(_attackData.laserPrefab,(int)PoolingIndex.PlayerUlt);
        Vector3 pos = _player.transform.position;
        pos += 2 * (_spriteRenderer.flipX ? Vector3.left : Vector3.right);
        go.transform.position = pos;
        //방향 설정
        float dir = _spriteRenderer.flipX ? -180 : 0;
        go.transform.rotation = Quaternion.Euler(0, 0, dir);
        
        //필살기 데이터 설정
        go.TryGetComponent(out UltimateAttack ultimateAttack);
        ultimateAttack.WeaponInfo = _player.WeaponInfo;
        ultimateAttack.UltAttackInit(_hitCount,_interval);
        
        //필살기 실행
        ultimateAttack.UltAttack();
        
        //필살기 시간동안 정지
        yield return new WaitForSeconds(_hitCount * _interval);
        
        //실행
        _player.Animator.speed = 1;
        _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public void HitCountUp(int count)
    {
        _hitCount = count;
    }

}
