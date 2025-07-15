using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : BossBaseState
{
    private float _time;

    public BossSpawnState(BossStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        //콜라이더 형태 변경
        UpdateCollider();
        
        //빠르게 떨어지기
        _rigidbody.gravityScale = 15f;
        
        //애니메이션 실행
        _boss.Animator.SetTrigger(_boss.AnimationDB.SpawnParameterHash);

        //애니메이션 실행에 필요한 사운드
        SoundClip[0] = _data.landingSound;
        SoundClip[1] = _data.breathSound;
        SoundClip[2] = _data.howlingSound;
    }

    public override void Exit()
    {
        //중력 복구
        _rigidbody.gravityScale = 3f;
        
        //플레이어 상태 복구
        _boss.BossEvent.StartBattle();
        
        //배경음 추가
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialBossPhase1);
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
        //콜라이더 자동 변경 시 콜라이더가 사라지는 버그가 있어서 블락
    }
}