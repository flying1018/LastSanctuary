using UnityEngine;


public class EAttackState : EnemyBaseState
{
    public EAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    private float _time;
    private float _animtionTime;


    public override void Enter()
    {
        //공격에 관련된 정보 초기화
        _time = 0;
        _animtionTime = _data.AnimTime;
        _stateMachine.attackCoolTime = 0;

        //공격 중간은 Idle 애니메이션
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.AttackParameterHash);

        //공격력 정보 넘겨주기
        _enemy.EnemyWeapon.Damage = _data.attack;
        _enemy.EnemyWeapon.KnockBackForce = _data.knockbackForce;
    }

    public override void Exit()
    {
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        //에니메이션이 끝나야 쿨타임 체크
        _time += Time.deltaTime;
        if (_time < _animtionTime)
            return;

        base.Update();

        //공격 쿨타임이 지나면
        if (_data.attackDuration < _stateMachine.attackCoolTime)
        {
            //타겟이 없으면 복귀
            if (!_enemy.FindTarget())
                _stateMachine.ChangeState(_stateMachine.ReturnState);
            //아직 사정거리 안이라면 다시 공격
            else
                _stateMachine.ChangeState(_stateMachine.AttackState);

        }

        //추적범위 밖에 있으면 추적
        if (!WithinChaseDistance())
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //타겟을 향해서 보기
        Vector2 direction = DirectionToTarget();
        Rotate(direction);
    }
}

public class EnemyAttackState : EAttackState
{
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
   
}

