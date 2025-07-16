using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseShiftState : BossBaseState
{

    private float _time;
    public BossPhaseShiftState(BossStateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        //애니메이션 시작
        _boss.Animator.SetTrigger(_boss.AnimationDB.PhaseShiftParameterHash);

        //시간 초기화 
        _time = 0;
        
        //페이즈 변경
        _boss.Phase2 = true;
        
        //효과음 실행
        PlaySFX1();
        
        //배경음 변경
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialPhaseShift);
    }

    public override void Exit()
    {
        
        //추가 공격 바로 실행을 위해서 추가
        _stateMachine.Attacks.Clear();
        _stateMachine.Attacks.Enqueue(_stateMachine.Attack3);

        //외곽선 변경
        _spriteRenderer.material = _data.materials[1];
        
        //배경음 변경
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialBossPhase2);
    }

    public override void Update()
    {
        //시간이 끝나면
        _time += Time.deltaTime;
        if(_time > _data.PhaseShiftTime)
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {
        //콜라이더 자동 변경 시 콜라이더가 사라지는 버그가 있어서 블락
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.phaseShiftSound);
    }
}
