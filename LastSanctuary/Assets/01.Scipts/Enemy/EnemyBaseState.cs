
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine stateMachine;
    protected EnemySO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected EnemyCondition _condition;
    protected GameObject _enemyModel;
    protected Enemy _enemy;
    
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this.stateMachine = enemyStateMachine;
        _enemy = stateMachine.Enemy;
        _data = _enemy.Data;
        _rigidbody = _enemy.Rigidbody;
        _spriteRenderer =_enemy.SpriteRenderer;
        _enemyModel = _enemy.Model;
        _condition = _enemy.Condition;
        
    }
    
    public virtual void Enter()
    {
    }
    public virtual void Exit()
    {
    }
    public virtual void HandleInput()
    {
    }
    public virtual void Update()
    {
    }
    public virtual void PhysicsUpdate()
    {
    }
    protected void Move()
    {
    }
    private void Move(Vector3 moveDirection)
    {
    }
}
