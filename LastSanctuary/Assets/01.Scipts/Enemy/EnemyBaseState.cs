
using UnityEngine;

public class EnemyBaseState : IState
{
    protected EnemyStateMachine _stateMachine;
    protected EnemySO _data;
    protected Rigidbody2D _rigidbody;
    protected SpriteRenderer _spriteRenderer;
    protected EnemyCondition _condition;
    protected Enemy _enemy;
    protected Transform _spawnPoint;
    
    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this._stateMachine = enemyStateMachine;
        _enemy = _stateMachine.Enemy;
        _data = _enemy.Data;
        _rigidbody = _enemy.Rigidbody;
        _spriteRenderer =_enemy.SpriteRenderer;
        _condition = _enemy.Condition;
        _spawnPoint = _enemy.SpawnPoint;
        
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
    protected void Move(Vector2 direction)
    {
        float xDirection = direction.x > 0 ? 1 : direction.x < 0 ? -1 : 0;
        Vector2 moveVelocity = new Vector2(xDirection * _data.moveSpeed, _rigidbody.velocity.y);
        _rigidbody.velocity = moveVelocity;
    }

    protected void Rotate(Vector2 direction)
    {
        _spriteRenderer.flipX = direction.x < 0;
    }
}
