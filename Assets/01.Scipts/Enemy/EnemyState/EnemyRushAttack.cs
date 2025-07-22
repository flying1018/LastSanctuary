using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRushAttack : EAttackState
{
    public EnemyRushAttack(EnemyStateMachine enemyStateMachine) : base(enemyStateMachine)
    {
    }

    public override void Enter()
    {
        //공격에 관련된 정보 초기화
        _time = 0;
        _stateMachine.attackCoolTime = 0;
        
        //애니메이션 시작
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.AttackParameterHash);
        
        //공격력 정보 넘겨주기
        _enemy.EnemyWeapon.Condition = _condition;
        _enemy.EnemyWeapon.Damage = _data.attack;
        _enemy.EnemyWeapon.knockBackForce = _data.knockbackForce;
        if (_enemy.EnemyWeapon is RushWeapon rushWeapon)
        {
            rushWeapon.RushEnemy = this;
        }
    }

    public override void Exit()
    {
        Move(Vector2.zero);
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }


    public override void PlayEvent1()
    {
        RushAttack();
    }

    //플레이어를 향해서 돌진
    public void RushAttack()
    {
        Vector2 direction = DirectionToTarget();
        Rotate(direction);
        _move.AddForce(direction * _data.rushSpeed);
    }

    //충돌 시 정지
    public void RushKnuckBack(Transform target, float knockBackForce)
    {
        Vector2 knockbackDir = _enemy.transform.position - target.position;
        _move.AddForce(knockbackDir.normalized * knockBackForce);

        if (_enemy.StateMachine.currentState is EnemyGroggyState) return;
        _stateMachine.ChangeState(_stateMachine.BattleState);
    }
}
