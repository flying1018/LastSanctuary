using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackState : PlayerAttackState
{
    private AttackInfo _attackInfo;
    private float _animationTime;
    private float _time;
    
    public JumpAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        //현재 공격 정보 가져오기
        int comboIndex = _stateMachine.comboIndex;
        _attackInfo = _data.attacks.GetAttackInfo(comboIndex);
        
        //공격 애니메이션 실행
        _player.Animator.SetInteger(_player.AnimationDB.ComboParameterHash, _attackInfo.attackIndex);

        //시간 측정
        _time = 0;
        _animationTime = _player.Animator.GetCurrentAnimatorStateInfo(0).length;
    }

    public override void Exit()
    {
        base.Exit();
        
        _player.Animator.SetInteger(_stateMachine.Player.AnimationDB.ComboParameterHash, 0);
    }

    public override void HandleInput()
    {
        
    }

    public override void Update()
    {
        _time += Time.deltaTime;
        //공격 종료
        if (_time > (_animationTime + _attackInfo.nextComboTime))
        {
            _stateMachine.comboIndex = 0;
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }

    public override void PhysicsUpdate()
    {

    }
}
