using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if(_enemy.Target == null)
            _stateMachine.ChangeState(_stateMachine.IdleState);
    }

    public override void PhysicsUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        if(_enemy.Target == null) return;
        
        Vector2 direction = _enemy.Target.position - _enemy.transform.position;
        if (Mathf.Abs(direction.x) < 1f) return;
        Move(direction);
        Rotate(direction);
    }
}
