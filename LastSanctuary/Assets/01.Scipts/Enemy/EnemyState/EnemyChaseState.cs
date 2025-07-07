using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private float _time;

    public EnemyChaseState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        
        _time = 0;
        
    }

    public override void Exit()
    {
        Move(Vector2.zero);
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }

    public override void Update()
    {
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
        Chase();
    }

    private void Chase()
    {
        if(_enemy.Target == null) return;
        if (!_enemy.IsPlatform())
        {
            Move(Vector2.zero);
            Rotate(Vector2.zero);
            return;
        }

        Vector2 direction = DirectionToTarget();
        Move(direction);
        Rotate(direction);
    }
    
}
