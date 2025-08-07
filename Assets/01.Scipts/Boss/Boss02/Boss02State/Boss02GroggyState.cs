using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02GroggyState : BossBaseState
{
    
    private float _groggyStart;
    private Color _originColor;
    private float _blinkLength = 2;
    
    public Boss02GroggyState(Boss02StateMachine bossStateMachine) : base(bossStateMachine) { }

    public override void Enter()
    {
        //시간 설정
        _groggyStart = Time.time;
        
        //그로기 시 대미지 증가를 위한 표시
        _condition2.IsGroggy = true;
        
        //애니 실행
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.GroggyEnterParameter);
        
        //색상 저장
        _originColor = _boss2.SpriteRenderer.color;
    }

    public override void Exit()
    {
        //그로기 탈출
        _condition2.IsGroggy = false;
        
        //색상 변경
        _boss2.SpriteRenderer.color = _originColor;
        
        _boss2.Animator.SetTrigger(_boss2.AnimationDB.GroggyExitParameter);
    }

    public override void Update()
    {
        _condition2.GroggyGauge -= (_condition2.MaxGroggyGauge/_data.groggyDuration)*Time.deltaTime;
        
        //색상 점멸
        float blinking = Mathf.PingPong(Time.time * _data.groggyDuration, _blinkLength);
        _boss2.SpriteRenderer.color = Color.Lerp(Color.red,_originColor, blinking);
        
        //애니메이션 그로기 시간 끝나기 까지 기다리기
        if (Time.time - _groggyStart < _data.groggyDuration)
            return;
        
        //체력이 페이즈 변환 조건일 만족했을 때
        if (_condition2.CheckPhaseShift())
        {   //페이즈 변환
            _stateMachine2.ChangeState(_stateMachine2.PhaseShiftState);
        }
        //아니라면 대기
        else
        { 
            _stateMachine2.ChangeState(_stateMachine2.TeleportState);
        }
    }
}
