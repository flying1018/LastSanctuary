using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02SpawnState : BossBaseState
{
    public Boss02SpawnState(Boss02StateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        //외곽선 변경
        _spriteRenderer.material = _data.materials[0];
        
        //애니메이션 실행
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.SpawnParameterHash);
        
        //보스 콜라이더 켜기
        _boxCollider.enabled = true;

        _time = 0;
        
        PlaySFX1();
    }
    
    public override void Exit()
    {
        SoundManager.Instance.PlayBGM(BGM.Boss02_Bgm);
        
        //bossUI
        if (_boss2.UIOn)
        {
            UIManager.Instance.SetBossUI(true,_condition2);
        }
    }
    
    public override void Update()
    {
        //스폰 애니메이션 시간이 끝나면
        _time += Time.deltaTime;
        if (_time >= _data.SpawnAnimeTime)
        {
            _boss02Event.EndZoomCamera();
            _boss2.StartCoroutine(StartBattle());
            
            _stateMachine2.MoveTarget = _boss02Event.TopMirror.position;
            _stateMachine2.ChangeState(_stateMachine2.TeleportState);
        }
    }

    public override void PhysicsUpdate()
    {
        Move(Vector2.down);
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.spawnSound);
    }

    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(1f);
        _boss02Event.StartBattle();
    }
}
