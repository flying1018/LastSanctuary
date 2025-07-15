using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyReturnState : EReturnState
{
    public EnemyFlyReturnState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Update()
    {
        //공격 쿨타임 체크
        base.Update();
        
        //스폰 포인트 위치와 나의 위치의 오차가 0.1f 라면
        if (Vector2.Distance(_enemy.transform.position, _spawnPoint.position) < 0.1f)
        {   //대기
            _stateMachine.ChangeState(_stateMachine.IdleState);       
        }
    }

    //스폰 포인트를 향해 이동
    protected override void Return()
    {
        Vector2 direction = DirectionToSpawnPoint();
        Fly(direction);
        Rotate(direction);
    }
}
