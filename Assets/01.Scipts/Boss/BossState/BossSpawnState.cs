using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnState : BossBaseState
{
    public BossSpawnState(BossStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        //외곽선 변경
        _spriteRenderer.material = _data.materials[0];
        
        //빠르게 떨어지기
        _move.gravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        
        //애니메이션 실행
        _boss.Animator.SetTrigger(_boss.AnimationDB.SpawnParameterHash);
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
        Move(_move.gravityScale);
    }

    public override void PlayEvent1()
    {
        LandingCameraShake();
    }
    
    public override void PlayEvent2()
    {
        HowlingCameraShake();
    }

    //카메라 흔들기
    public void LandingCameraShake()
    {
        _boss.BossEvent.CameraShake();
    }
    public void HowlingCameraShake()
    {
        _boss.BossEvent.CameraShake(_data.howlingSound.length/2); 
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.landingSound);
    }
    
    public override void PlaySFX2()
    {
        SoundManager.Instance.PlaySFX(_data.breathSound);
    }
    
    public override void PlaySFX3()
    {
        SoundManager.Instance.PlaySFX(_data.howlingSound);
    }
}