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
        if(_enemy.Target == null)
            _stateMachine.ChangeState(_stateMachine.ReturnState);
        
        //공격 조건
        float targetDistance = TargetDistance();
        if (targetDistance < _data.attackDistance/2 && _attacktCoolTime > _data.attackDuration)
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
        

        CancelChase();

    }

    public override void PhysicsUpdate()
    {
        Chase();
    }

    private void Chase()
    {
        if(_enemy.Target == null) return;
        
        Vector2 direction = _enemy.Target.position - _enemy.transform.position;
        _enemy.IsRight = direction.x > 0 ? true : false;
        if (Mathf.Abs(direction.x) < 1f) return;
        Move(direction);
        Rotate(direction);
    }

    private void CancelChase()
    {
        //움직이지 않기 시작한다면 시간 체크
        if (_rigidbody.velocity.magnitude < 0.1f)
        {
            _time += Time.deltaTime;
        }
        //다시 움직인다면 시간체크 취소
        else
        {
            _time = 0;
        }
        
        //취소 시간에 도달하거나 플랫폼 끝에 있을 경우
        if (_time > _data.cancelChaseTime || !_enemy.IsPlatform())
        {
            _stateMachine.ChangeState(_stateMachine.ReturnState);       
        }
    }
}
