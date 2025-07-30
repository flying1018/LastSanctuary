using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackState : PlayerAttackState
{
    public ComboAttackState(PlayerStateMachine stateMachine, AttackInfo attackInfo) : base(stateMachine, attackInfo) { }
    
    public override void HandleInput()
    {
        //대쉬나 가드로 전환 가능
        base.HandleInput();
        
        //다음 공격
        //입력 시간 내에 공격 입력 시
        _time += Time.deltaTime;
        if (_time <= (_animationTime + AttackInfo.nextComboTime) && _input.IsAttack)
        {
            //애니메이션 끝나고 공격
            if (_time > _animationTime)
            {
                //다음 공격 번호 가져오기
                _stateMachine.comboIndex =
                    _stateMachine.ComboAttack.Count <= _stateMachine.comboIndex + 1
                        ? 0 : _stateMachine.comboIndex + 1;

                int cost = _stateMachine.ComboAttack[_stateMachine.comboIndex].StaminaCost;
                Debug.Log(cost);
                //다음 공격의 필요 스테미나가 충분하다면 공격
                if (_condition.UsingStamina(cost))
                {
                    //다음 공격이 없으면 종료
                    if (_stateMachine.comboIndex == 0)
                    {
                        _stateMachine.ChangeState(_stateMachine.IdleState);
                        return;
                    }

                    //다음 공격
                    _stateMachine.ChangeState(_stateMachine.ComboAttack[ _stateMachine.comboIndex]);
                }
            }

        }
        //공격 종료
        else if (_time > (_animationTime + AttackInfo.nextComboTime))
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
