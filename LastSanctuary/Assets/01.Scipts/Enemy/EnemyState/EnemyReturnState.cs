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
        if (Mathf.Abs(_enemy.transform.position.x - _spawnPoint.position.x) < 0.5f)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);       
        }
    }

    public void Return()
    {
        Vector2 direction = _spawnPoint.position - _enemy.transform.position;
        Rotate(direction);
        Move(direction);
    }
}
