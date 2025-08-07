using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02PhaseShiftState : BossBaseState
{
    public Boss02PhaseShiftState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }
    
    public override void Enter()
    {
        //애니메이션 시작
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.PhaseShiftParameterHash);
        
        //시간 초기화 
        _time = 0;
        
        //페이즈 변경
        _boss2.Phase2 = true;
        
        //배경음 변경
        SoundManager.Instance.PlayBGM(StringNameSpace.SoundAddress.TutorialPhaseShift);
    }

    public override void Exit()
    {
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
        {   
            SetMovePosition();
            _stateMachine2.ChangeState(_stateMachine2.TeleportState);
        }
    }
}
