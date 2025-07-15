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
        StartAnimation(_enemy.AnimationDB.WalkParameterHash);;
        _enemy.Animator.SetTrigger(_enemy.AnimationDB.AttackParameterHash);
    }

    public override void Exit()
    {
        Move(Vector2.zero);
        StopAnimation(_enemy.AnimationDB.WalkParameterHash);
    }


    //플레이어를 향해서 돌진
    public void RushAttack()
    {
        Vector2 direction = DirectionToTarget();
        Rotate(direction);
        _rigidbody.AddForce(direction * _data.rushSpeed, ForceMode2D.Impulse);
    }

    //충돌 시 정지
    public void RushKnuckBack(Transform target, float knockBackForce)
    {
        Vector2 knockbackDir = _enemy.transform.position - target.position;
        _rigidbody.velocity = knockbackDir.normalized * knockBackForce;
        _stateMachine.ChangeState(_stateMachine.BattleState);
    }
}
