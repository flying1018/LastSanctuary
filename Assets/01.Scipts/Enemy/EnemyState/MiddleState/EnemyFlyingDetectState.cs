using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//돌진 몬스터 용
public class EnemyFlyingDetectState : EDetectState
{
    public EnemyFlyingDetectState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) { }

    public override void Update()
    {
        base.Update();
        
        //인식 상태가 끝나면
        _time += Time.deltaTime;
        if (_time > _data.detectTime && _enemy.Target != null)
        {   //공격
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
    }
}
