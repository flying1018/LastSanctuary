using UnityEngine;

public class EReturnState : EnemyBaseState
{
    public EReturnState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _condition.IsInvincible = true;
        _enemy.IsRight = !_enemy.IsRight;
        _enemy.Target = null;  
    }

    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _condition.IsInvincible = false;
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        Return();
    }

    protected virtual void Return()
    {
        
    }
    
}


public class EnemyReturnState :EReturnState
{
    public EnemyReturnState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x) < 0.1f)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);       
        }
    }

    protected override void Return()
    {
        Vector2 direction = DirectionToSpawnPoint();
        Move(direction);
        Rotate(direction);
    }
}
