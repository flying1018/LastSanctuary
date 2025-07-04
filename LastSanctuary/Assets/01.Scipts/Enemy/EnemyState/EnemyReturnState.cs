using UnityEngine;

public class EnemyReturnState :EnemyBaseState
{
    public EnemyReturnState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _condition.IsInvincible = true;
        _enemy.IsRight = !_enemy.IsRight;
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

    public override void Update()
    {
        base.Update();
        if (Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x) < 1f)
        {
            Debug.Log(Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x));
            _stateMachine.ChangeState(_stateMachine.IdleState);       
        }
    }

    public void Return()
    {
        Vector2 direction = DirectionToSpawnPoint();
        Rotate(direction);
        Move(direction);
    }
}
