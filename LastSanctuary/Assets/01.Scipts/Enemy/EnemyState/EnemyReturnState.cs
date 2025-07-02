using UnityEngine;

public class EnemyReturnState :EnemyBaseState
{
    public EnemyReturnState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Return Enter");
        _condition.IsInvincible = true;
        _enemy.IsRight = !_enemy.IsRight;
    }

    public override void Exit()
    {
        Debug.Log("Return Exit");
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
