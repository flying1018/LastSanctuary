using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float _time;
    private float _animtionTime;
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }

    
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
    }

    public override void Exit()
    {
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
    }

    public override void Update()
    {
        //에니메이션이 끝나야 쿨타임 체크
        _time += Time.deltaTime;
        if (_time > _animtionTime)
        {
            base.Update();
        }
        
        //타겟이 아직 사정거리 밖에 있으면 추적
        float targetDistance = TargetDistance();
        if (targetDistance > _data.attackDistance / 2)
        {
            _stateMachine.ChangeState(_stateMachine.ChaseState);   
        }
        
        //공격 쿨타임이 지나면
        if (_data.attackDuration < _stateMachine.attackCoolTime)
        {
            //타겟이 없으면 복귀
            if(_enemy.Target == null)
                _stateMachine.ChangeState(_stateMachine.ReturnState);
            else
            {   //아직 사정거리 안이라면 다시 공격
                _stateMachine.ChangeState(_stateMachine.AttackState);   
            }
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
