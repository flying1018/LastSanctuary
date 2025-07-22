using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroggyState : EnemyBaseState
{
    private Color _originColor;

    public EnemyGroggyState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.GroggyParameterHash);

        _time = 0;
        
        //색상 저장
        _originColor = _enemy.SpriteRenderer.color;
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.GroggyParameterHash);
        
        _enemy.SpriteRenderer.color = _originColor;
    }

    public override void Update()
    {
        base.Update();
        
        _time += Time.deltaTime;
        
        //색상 점멸
        float blinking = Mathf.PingPong(_time * _condition.GroggyTime, _data.blinkLength);
        //숫자 1로 기준으로 변색 됨으로 계산
        blinking *= (1/ _data.blinkLength);
        _enemy.SpriteRenderer.color = Color.Lerp(Color.red, _originColor, blinking);
        
        //시간 종료 시 다시 전투
        if (_time > _condition.GroggyTime)
        {
            _stateMachine.ChangeState(_stateMachine.BattleState);
        }
    }


}
