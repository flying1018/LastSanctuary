using UnityEngine;

public class EChaseState : EnemyBaseState
{
    public EChaseState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        //이동 애니메이션
        _moveSpeed = _enemy.Data.moveSpeed;
        if (_enemy.Data.runSpeed > 0)
        {
            StartAnimation(_enemy.AnimationDB.RunParameterHash);
           _moveSpeed = _enemy.Data.runSpeed; 
        }
        else
            StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _time = 0;
        
    }
    
    public override void Exit()
    {
        //이동 애니메이션
        if (_enemy.Data.runSpeed > 0)
        {
            StopAnimation(_enemy.AnimationDB.RunParameterHash);
            _moveSpeed = _enemy.Data.moveSpeed;
        }
        else
            StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }
    
    public override void Update()
    {
        //공격 쿨타임 체크
        base.Update();
        
        //복귀 조건
        //1. 추적 시간이 초과 했거나
        _time += Time.deltaTime;
        if(_time > _data.cancelChaseTime)
            _stateMachine.ChangeState(_stateMachine.ReturnState);
        //추적 종료 조건
        //1. 타겟이 추적 범위안에 도달 했을 때
        if (WithinChaseDistance())
        {
            _stateMachine.ChangeState(_stateMachine.BattleState);
        }

    }
    
    public override void PhysicsUpdate()
    {
        //추적
        Chase();
    }

    //추적 가상 클래스
    protected virtual void Chase()
    {
        
    }

    public override void PlaySFX1()
    {
        SoundManager.Instance.PlaySFX(_data.walkSound);
    }
}


public class EnemyChaseState : EChaseState
{
    public EnemyChaseState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    protected override void Chase()
    {   
        if(_enemy.Target == null) return;
        
        //절벽에 다다르면 정지
        if (!_enemy.IsPlatform())
        {
            Move(Vector2.zero);
            Rotate(Vector2.zero);
            return;
        }

        //타겟을 향해 이동
        Vector2 direction = DirectionToTarget();
        
        //좌우 거리가 너무 가까우면 이동하지 않음.
        if (Mathf.Abs(direction.x) < 0.05) return;

        Rotate(direction);
        Move(direction);
        
    }
    
}
