using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackState : PlayerAttackState
{
    private AttackInfo _attackInfo;
    private float _animationTime;
    private float _time;
    
    public ComboAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        //현재 공격 정보 가져오기
        int comboIndex = _stateMachine.comboIndex;
        _attackInfo = _stateMachine.Player.Data.attacks.GetAttackInfo(comboIndex);
        
        //공격 애니메이션 실행
        _stateMachine.Player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, _attackInfo.attackIndex);

        //시간 측정
        _time = 0;
        _animationTime = _stateMachine.Player.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public override void Exit()
    {
        base.Exit();
        
        _stateMachine.Player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
    }

    public override void HandleInput()
    {
        //다음 공격
        _time += Time.deltaTime;
        
        //입력 시간 내에 공격 입력 시
        if (_time <= (_animationTime + _attackInfo.nextComboTime) && _input.IsAttack)
        {
            //애니메이션 끝나고 공격
            if (_time > _animationTime)
            {
                //다음 공격 번호 가져오기
                _stateMachine.comboIndex =
                    _stateMachine.Player.Data.attacks.GetAttackInfoCount()  <= _stateMachine.comboIndex + 1
                        ? 0
                        : _stateMachine.comboIndex + 1;
                //다음 공격이 없으면 종료
                if (_stateMachine.comboIndex == 0)
                {
                    _input.IsAttack = false;
                    _stateMachine.ChangeState(_stateMachine.IdleState);
                    return;
                }

                //다음 공격
                _stateMachine.ChangeState(_stateMachine.ComboAttack);
            }
            
        }
        //공격 종료
        else if (_time > (_animationTime + _attackInfo.nextComboTime))
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
        
    }

    public override void Update()
    {
    }

    public override void PhysicsUpdate()
    {

    }
}
