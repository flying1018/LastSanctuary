
using System.Collections.Generic;
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
    protected KinematicMove _move;

    protected float _time;
    protected float _moveSpeed;
    


    public EnemyBaseState(EnemyStateMachine enemyStateMachine)
    {
        this._stateMachine = enemyStateMachine;
        _enemy = _stateMachine.Enemy;
        _data = _enemy.Data;
        _rigidbody = _enemy.Rigidbody;
        _spriteRenderer =_enemy.SpriteRenderer;
        _condition = _enemy.Condition;
        _spawnPoint = _enemy.SpawnPointPos;
        _move = _enemy.Move;
        _moveSpeed = _data.moveSpeed;
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
        //공격 쿨타임 체크
        _stateMachine.attackCoolTime += Time.deltaTime;
    }
    public virtual void PhysicsUpdate()
    {
    }


    
    protected void StartAnimation(int animatorHash)
    {
        _enemy.Animator.SetBool(animatorHash, true);
    }

    protected void StopAnimation(int animatorHash)
    {
        _enemy.Animator.SetBool(animatorHash, false);
    }
    
    //좌우 이동
    protected void Move(Vector2 direction)
    {
        Vector2 x = _move.Horizontal(direction, _moveSpeed);
        
        if(!_move.IsGrounded)
            _move.gravityScale += _move.Vertical(Vector2.down, _data.gravityPower);
        else
            _move.gravityScale = Vector2.zero;
        
        
        _move.Move(x + _move.gravityScale);
    }

    //회전
    protected void Rotate(Vector2 direction)
    {
        if(direction == Vector2.zero) return;
        _spriteRenderer.flipX = direction.x < 0;
        //무기 회전
        float angle = _spriteRenderer.flipX ? 180 : 0;
        _enemy.Weapon.transform.rotation = Quaternion.Euler(angle, 0, angle);
    }
    
    //공격 사정거리 1/2 만큼 접근
    protected bool WithinChaseDistance()
    {
        if (_enemy.Target == null) return false;
        
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.transform.position);
        return distance <= _data.attackDistance/2;
    }

    //공격 범위 안에 있으면 공격
    protected bool WithinAttackDistnace()
    {
        if (_enemy.Target == null) return false;
        
        float distance = Vector2.Distance(_enemy.transform.position, _enemy.Target.transform.position);
        return distance <= _data.attackDistance;
    }

    //타겟의 방향을 구하는 코드
    protected Vector2 DirectionToTarget()
    {
        if(_enemy.Target == null) return Vector2.zero;
        return (_enemy.Target.position  - _enemy.transform.position).normalized;
    }

    //스폰 포인트 방향을 구하는 코드
    protected Vector2 DirectionToSpawnPoint()
    {
        return (_spawnPoint.position - _enemy.transform.position).normalized;
    }
    
    #region AnimationEvent Method

    public virtual void PlayEvent1() { }

    public virtual void PlaySFX1() { }
    public virtual void PlaySFX2() { }
    
    #endregion
}
