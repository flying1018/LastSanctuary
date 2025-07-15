using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EIdleState
{
    public EnemyPatrolState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine) {}
    
    private float _patrolDistance;

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _patrolDistance = _enemy.PatrolDistance;
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }
    
    public override void PhysicsUpdate()
    {
        Patrol();
    }

    //순회
    public void Patrol()
    {
        
        float movedDistance = Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x);
        bool distanceExit = movedDistance >= _patrolDistance;
        
        //플립을 기준으로 방향을 정하는데 그 플립을 이용해서 회전을 시도하면 문제가 있어보임.
        Vector2 direction =_enemy.SpriteRenderer.flipX ? Vector2.left : Vector2.right;
        if (!_enemy.IsPlatform() || distanceExit)
        {
            Rotate(-direction);
            Move(-direction);
            return;
        }
        
        Move(direction);
        Rotate(direction);
    }
}
