using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02SpawnState : Boss02BaseState
{
    public Boss02SpawnState(Boss02StateMachine stateMachine) : base(stateMachine) { }
    
    public override void Enter()
    {
        //외곽선 변경
        _spriteRenderer.material = _data.materials[0];
        
        //애니메이션 실행
        _boss.Animator.SetTrigger(_boss.AnimationDB.SpawnParameterHash);
        
        //보스 콜라이더 켜기
        _boxCollider.enabled = true;
    }
    
    public override void Exit()
    {
        //플레이어 상태 복구
        _boss.BossEvent.StartBattle();
        
        //배경음 추가
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialBossPhase1);
        
        //bossUI
        if (_boss.UIOn)
        {
            UIManager.Instance.SetBossUI(true,_condition);
        }
    }
    
    public override void Update()
    {
        //스폰 애니메이션 시간이 끝나면
        _time += Time.deltaTime; 
        if (_time >= _data.SpawnAnimeTime)
        {
            //대기 상태
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        Move(Vector2.down);
    }
    
    
}
