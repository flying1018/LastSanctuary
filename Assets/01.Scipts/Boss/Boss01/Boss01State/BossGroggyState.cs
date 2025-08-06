using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGroggyState : BossBaseState
{
    private float _groggyStart;
    private Color _originColor;
    private float _blinkLength = 2;
    
    public BossGroggyState(BossStateMachine bossStateMachine1) : base(bossStateMachine1) { }

    public override void Enter()
    {
        //시간 설정
        _groggyStart = Time.time;
        
        //그로기 시 대미지 증가를 위한 표시
        _condition.IsGroggy = true;
        
        //애니 실행
       _boss.Animator.SetTrigger(_boss.AnimationDB.GroggyParameterHash);
       StartAnimation(_boss.AnimationDB.GroggyParameterHash);
        
        //색상 저장
        _originColor = _boss.SpriteRenderer.color;
    }

    public override void Exit()
    {
        //그로기 탈출
        _condition.IsGroggy = false;
        
        //색상 변경
        _boss.SpriteRenderer.color = _originColor;
        
        StopAnimation(_boss.AnimationDB.GroggyParameterHash);
    }

    public override void Update()
    {
        _condition.GroggyGauge -= (_condition.MaxGroggyGauge/_data.groggyDuration)*Time.deltaTime;
        
        //색상 점멸
        float blinking = Mathf.PingPong(Time.time * _data.groggyDuration, _blinkLength);
        _boss.SpriteRenderer.color = Color.Lerp(Color.red,_originColor, blinking);
        
        //애니메이션 그로기 시간 끝나기 까지 기다리기
        if (Time.time - _groggyStart < _data.groggyDuration)
            return;
        
        //체력이 페이즈 변환 조건일 만족했을 때
        if (_condition.CheckPhaseShift())
        {   //페이즈 변환
            _stateMachine1.ChangeState(_stateMachine1.PhaseShiftState);
        }
        //아니라면 대기
        else
        { 
            _stateMachine1.ChangeState(_stateMachine1.IdleState);
        }
    }
}
