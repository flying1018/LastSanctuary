using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private float _time;
    private bool _dontMove;
    public EnemyChaseState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    public override void Enter()
    {
        _dontMove = false;
        _time = 0;
    }

    public override void Exit()
    {

    }

    public override void Update()
    {
        if(_enemy.Target == null)
            _stateMachine.ChangeState(_stateMachine.ReturnState);

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
        if (_rigidbody.velocity.magnitude < 0.1f || !_enemy.IsPlatform())
        {
            _time += Time.deltaTime;
        }
        //다시 움직인다면 시간체크 취소
        else
        {
            _time = 0;
        }
        
        //취소 시간에 도달하면 복귀
        if (_time > _data.cancelChaseTime)
        {
            _stateMachine.ChangeState(_stateMachine.ReturnState);       
        }
    }
}
