using UnityEngine;


public class EAttackState : EnemyBaseState
{
    protected float _time;

    public EAttackState(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }
    
    public override void Enter()
    {
        //공격에 관련된 정보 초기화
        _time = 0;
        _stateMachine.attackCoolTime = 0;

        //공격 중간은 Idle 애니메이션
        StartAnimation(_enemy.AnimationDB.IdleParameterHash);
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.AttackParameterHash);

        //공격력 정보 넘겨주기
        _enemy.EnemyWeapon.Damage = _data.attack;
        _enemy.EnemyWeapon.knockBackForce = _data.knockbackForce;

        //효과음 추가
        SoundClip[0] = _data.attackSound;
    }
    
    public override void Exit()
    {
        StopAnimation(_enemy.AnimationDB.IdleParameterHash);
    }
    
    public override void Update()
    {
        //에니메이션이 끝나야 쿨타임 체크
        _time += Time.deltaTime;
        if (_time < _data.AttackTime)
            return;

        //공격 쿨타임 체크
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
}

public class EnemyAttackState : EAttackState
{
    public EnemyAttackState(EnemyStateMachine ememyStateMachine) : base(ememyStateMachine)
    {
    }
    
}

