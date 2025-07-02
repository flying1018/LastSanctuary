using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) {}
    
    private float _patrolDistance;
    private bool isRight;

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _patrolDistance = _data.patrolDistance;
        isRight = true;
        Debug.Log("Patrol Enter");
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }
    
    public override void Update()
    {
        patrol();

        if (_enemy.Target != null)
        {
            _stateMachine.ChangeState(_stateMachine.DetectState);
        }
    }

    public void patrol()
    {
        Vector2 pos =_enemy.transform.position;

        float movedDistance = Mathf.Abs(pos.x - _spawnPoint.transform.position.x);
        bool distanceExit = movedDistance >= _patrolDistance;

        if (!_enemy.IsPlatform() || distanceExit)
        {
            isRight = !isRight;
        }
        
        float dir = isRight ? 1 : -1;
        Vector2 moveVelocity = new Vector2(dir * _data.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }
}
