using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyReturnState : EReturnState
{
    // Start is called before the first frame update
    public EnemyFlyReturnState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Vector2.Distance(_enemy.transform.position, _spawnPoint.position) < 0.1f)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);       
        }
    }

    protected override void Return()
    {
        Vector2 direction = DirectionToSpawnPoint();
        Fly(direction);
        Rotate(direction);
    }
}
